/*
 * Tencent is pleased to support the open source community by making InjectFix available.
 * Copyright (C) 2019 THL A29 Limited, a Tencent company.  All rights reserved.
 * InjectFix is licensed under the MIT License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms.
 * This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
 */

using IFix.Core;
using System.Diagnostics;
using System.IO;
using UnityEngine;

// 跑不同仔细看文档Doc/example.md
public class Helloworld : MonoBehaviour
{
    // check and load patchs
    private void Start()
    {
        VirtualMachine.Info = (s) => UnityEngine.Debug.Log(s);

        //try to load patch for Assembly-CSharp.dll
        var patch = Resources.Load<TextAsset>("Assembly-CSharp.patch");
        if (patch != null)
        {
            PatchManager.Load(new MemoryStream(patch.bytes));
        }

        //try to load patch for Assembly-CSharp-firstpass.dll
        patch = Resources.Load<TextAsset>("Assembly-CSharp-firstpass.patch");
        if (patch != null)
        {
            PatchManager.Load(new MemoryStream(patch.bytes));
        }

        test();
    }

    [IFix.Patch]
    private void test()
    {
        var calc = new IFix.Test.Calculator();

        UnityEngine.Debug.Log("10 + 9 = " + calc.Add(10, 9));

        UnityEngine.Debug.Log("10 - 2 = " + calc.Sub(10, 2));
    }
}