# Friendly Custom Serializer Sample

This sample shows how to use a customized serializer with Friendly.  
Use it when you want to control an app with Friendly that has `EnableUnsafeBinaryFormatterSerialization` set to `false`.

**Background:** Friendly uses `BinaryFormatter` for inter process communication, so when `BinaryFormatter` cannot be used, you need to provide an alternative serializer.

## Usage

```csharp
WindowsAppFriend.SetCustomSerializer<CustomSerializer>();
```
Call this to register the serializer.

**Note:** The registered serializer will also be used inside the target process, so the assembly that defines it will be injected (DLL injection). Implement it as an assembly that is safe to run in the target process.

```csharp
public void Test()
{
    // Configure the custom serializer (call once at startup)
    WindowsAppFriend.SetCustomSerializer<CustomSerializer>();

    // Create WindowsAppFriend
    var app = new WindowsAppFriend(Process.Start(info));

    // Typical Friendly operations
    var formControls = app.AttachFormControls();
    formControls.button.EmulateClick();
    formControls.checkBox.EmulateCheck(CheckState.Checked);
    formControls.comboBox.EmulateChangeText("Item-3");
    formControls.comboBox.EmulateChangeSelect(2);
    formControls.radioButton1.EmulateCheck();
    formControls.radioButton1.EmulateCheck();
}
```

## Custom serializer

Implement `ICustomSerializer`.  
Any implementation style is fine; this sample uses [MessagePack](https://www.nuget.org/packages/MessagePack).

```csharp
public class IntPtrFormatter : IMessagePackFormatter<IntPtr>
{
    public void Serialize(ref MessagePackWriter writer, IntPtr value, MessagePackSerializerOptions options)
        => writer.Write(value.ToInt64());

    public IntPtr Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        => new IntPtr(reader.ReadInt64());
}

public class CustomSerializer : ICustomSerializer
{
    MessagePackSerializerOptions customOptions = MessagePackSerializerOptions
        .Standard
        .WithResolver(
            CompositeResolver.Create(
                new IMessagePackFormatter[] { new IntPtrFormatter() },
                new IFormatterResolver[] { TypelessContractlessStandardResolver.Instance }
            )
        );

    public object Deserialize(byte[] bin)
        => MessagePackSerializer.Typeless.Deserialize(bin, customOptions);

    public Assembly[] GetRequiredAssemblies() => [GetType().Assembly, typeof(MessagePackSerializer).Assembly];

    public byte[] Serialize(object obj)
        => MessagePackSerializer.Typeless.Serialize(obj, customOptions);
}
```