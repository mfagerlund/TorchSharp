// Copyright (c) .NET Foundation and Contributors.  All Rights Reserved.  See LICENSE in the project root for license information.
using System;
using System.Runtime.InteropServices;
using static TorchSharp.torch;

namespace TorchSharp
{
    using Modules;

    namespace Modules
    {
        /// <summary>
        /// This class is used to represent a Hardtanh module.
        /// </summary>
        public class Hardtanh : torch.nn.Module
        {
            internal Hardtanh(IntPtr handle, IntPtr boxedHandle) : base(handle, boxedHandle) { }

            [DllImport("LibTorchSharp")]
            private static extern IntPtr THSNN_Hardtanh_forward(torch.nn.Module.HType module, IntPtr tensor);

            public override Tensor forward(Tensor tensor)
            {
                var res = THSNN_Hardtanh_forward(handle, tensor.Handle);
                if (res == IntPtr.Zero) { torch.CheckForErrors(); }
                return new Tensor(res);
            }

            public override string GetName()
            {
                return typeof(Hardtanh).Name;
            }
        }
    }

    public static partial class torch
    {
        public static partial class nn
        {
            [DllImport("LibTorchSharp")]
            extern static IntPtr THSNN_Hardtanh_ctor(double min_val, double max_val, bool inplace, out IntPtr pBoxedModule);

            /// <summary>
            /// Hardtanh
            /// </summary>
            /// <param name="min_val">Minimum value of the linear region range.</param>
            /// <param name="max_val">Maximum value of the linear region range.</param>
            /// <param name="inplace">Do the operation in-place</param>
            /// <returns></returns>
            static public Hardtanh Hardtanh(double min_val = -1.0, double max_val = 1.0, bool inplace = false)
            {
                var handle = THSNN_Hardtanh_ctor(min_val, max_val, inplace, out var boxedHandle);
                if (handle == IntPtr.Zero) { torch.CheckForErrors(); }
                return new Hardtanh(handle, boxedHandle);
            }

            public static partial class functional
            {
                /// <summary>
                /// Hardtanh
                /// </summary>
                /// <param name="x">The input tensor</param>
                /// <param name="min_val">Minimum value of the linear region range.</param>
                /// <param name="max_val">Maximum value of the linear region range.</param>
                /// <param name="inplace">Do the operation in-place</param>
                /// <returns></returns>
                static public Tensor Hardtanh(Tensor x, double min_val = -1.0, double max_val = 1.0, bool inplace = false)
                {
                    using (var m = nn.Hardtanh(min_val, max_val, inplace)) {
                        return m.forward(x);
                    }
                }
            }
        }
    }
}
