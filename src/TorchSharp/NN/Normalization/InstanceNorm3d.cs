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
        /// This class is used to represent a InstanceNorm3D module.
        /// </summary>
        public class InstanceNorm3d : torch.nn.Module
        {
            internal InstanceNorm3d(IntPtr handle, IntPtr boxedHandle) : base(handle, boxedHandle)
            {
            }

            [DllImport("LibTorchSharp")]
            private static extern IntPtr THSNN_InstanceNorm3d_forward(IntPtr module, IntPtr tensor);

            public override Tensor forward(Tensor tensor)
            {
                if (tensor.Dimensions != 5) throw new ArgumentException($"Invalid number of dimensions for InstanceNorm argument: {tensor.Dimensions}");
                var res = THSNN_InstanceNorm3d_forward(handle.DangerousGetHandle(), tensor.Handle);
                if (res == IntPtr.Zero) { torch.CheckForErrors(); }
                return new Tensor(res);
            }
        }
    }

    public static partial class torch
    {
        public static partial class nn
        {
            [DllImport("LibTorchSharp")]
            extern static IntPtr THSNN_InstanceNorm3d_ctor(long features, double eps, double momentum, bool affine, bool track_running_stats, out IntPtr pBoxedModule);

            /// <summary>
            /// Applies Instance Normalization over a 5D input (a mini-batch of 3D inputs with additional channel dimension) as described in the paper Instance Normalization: The Missing Ingredient for Fast Stylization.
            /// </summary>
            /// <param name="features">C from an expected input of size (N,C,D,H,W)</param>
            /// <param name="eps">A value added to the denominator for numerical stability. Default: 1e-5</param>
            /// <param name="momentum">The value used for the running_mean and running_var computation. Can be set to None for cumulative moving average (i.e. simple average). Default: 0.1</param>
            /// <param name="affine">A boolean value that when set to True, this module has learnable affine parameters. Default: true</param>
            /// <param name="track_running_stats">A boolean value that when set to True, this module tracks the running mean and variance, and when set to False,
            /// this module does not track such statistics, and initializes statistics buffers running_mean and running_var as None.
            /// When these buffers are None, this module always uses batch statistics. in both training and eval modes. Default: true</param>
            /// <returns></returns>
            static public InstanceNorm3d InstanceNorm3d(long features, double eps = 1e-05, double momentum = 0.1, bool affine = true, bool track_running_stats = true)
            {
                unsafe {
                    var handle = THSNN_InstanceNorm3d_ctor(features, eps, momentum, affine, track_running_stats, out var boxedHandle);
                    if (handle == IntPtr.Zero) { torch.CheckForErrors(); }
                    return new InstanceNorm3d(handle, boxedHandle);
                }
            }
        }
    }
}
