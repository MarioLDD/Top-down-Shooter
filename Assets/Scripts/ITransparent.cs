using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransparent
{
    IEnumerator TransparentOn();

    IEnumerator TransparentOff();
}
