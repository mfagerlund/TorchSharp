// Copyright (c) .NET Foundation and Contributors.  All Rights Reserved.  See LICENSE in the project root for license information.

using static TorchSharp.torch;

namespace TorchSharp.torchvision
{
    internal class AutoContrast : ITransform
    {
        internal AutoContrast()
        {
        }

        public Tensor forward(Tensor input)
        {
            return transforms.functional.autocontrast(input);
        }
    }

    public static partial class transforms
    {
        /// <summary>
        /// Autocontrast the pixels of the given image
        /// </summary>
        /// <returns></returns>
        static public ITransform AutoContrast()
        {
            return new AutoContrast();
        }
    }
}
