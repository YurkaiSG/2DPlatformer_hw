using UnityEngine;

public class Player : MonoBehaviour
{
    private int _coinsAmount = 0;

    public void PickUpCoin(int coinsAmount)
    {
        _coinsAmount += coinsAmount;
        Debug.Log(_coinsAmount);
    }
}
