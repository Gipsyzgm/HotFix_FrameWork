// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: PbError.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace PbError {

  /// <summary>Holder for reflection information generated from PbError.proto</summary>
  public static partial class PbErrorReflection {

    #region Descriptor
    /// <summary>File descriptor for PbError.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PbErrorReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1QYkVycm9yLnByb3RvEgdQYkVycm9yIi4KDVNDX2Vycm9yX2NvZGUSEAoI",
            "UHJvdG9jb2wYASABKAUSCwoDTXNnGAIgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::PbError.SC_error_code), global::PbError.SC_error_code.Parser, new[]{ "Protocol", "Msg" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///公用错误提示
  /// </summary>
  public sealed partial class SC_error_code : pb::IMessage<SC_error_code> {
    private static readonly pb::MessageParser<SC_error_code> _parser = new pb::MessageParser<SC_error_code>(() => new SC_error_code());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SC_error_code> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::PbError.PbErrorReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SC_error_code() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SC_error_code(SC_error_code other) : this() {
      protocol_ = other.protocol_;
      msg_ = other.msg_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SC_error_code Clone() {
      return new SC_error_code(this);
    }

    /// <summary>Field number for the "Protocol" field.</summary>
    public const int ProtocolFieldNumber = 1;
    private int protocol_;
    /// <summary>
    ///协议号
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Protocol {
      get { return protocol_; }
      set {
        protocol_ = value;
      }
    }

    /// <summary>Field number for the "Msg" field.</summary>
    public const int MsgFieldNumber = 2;
    private string msg_ = "";
    /// <summary>
    ///错误消息(只用于方便调试,客户端不要用此信息直接输出错误提示)
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Msg {
      get { return msg_; }
      set {
        msg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SC_error_code);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SC_error_code other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Protocol != other.Protocol) return false;
      if (Msg != other.Msg) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Protocol != 0) hash ^= Protocol.GetHashCode();
      if (Msg.Length != 0) hash ^= Msg.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Protocol != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Protocol);
      }
      if (Msg.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Msg);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Protocol != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Protocol);
      }
      if (Msg.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Msg);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SC_error_code other) {
      if (other == null) {
        return;
      }
      if (other.Protocol != 0) {
        Protocol = other.Protocol;
      }
      if (other.Msg.Length != 0) {
        Msg = other.Msg;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Protocol = input.ReadInt32();
            break;
          }
          case 18: {
            Msg = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
