//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Substrate.NetApi.Attributes;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Metadata.V14;
using System.Collections.Generic;


namespace Substrate.Hexalem.NET.NetApiExt.Generated.Model.frame_system.extensions.check_non_zero_sender
{
    
    
    /// <summary>
    /// >> 145 - Composite[frame_system.extensions.check_non_zero_sender.CheckNonZeroSender]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class CheckNonZeroSender : BaseType
    {
        
        public override string TypeName()
        {
            return "CheckNonZeroSender";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
