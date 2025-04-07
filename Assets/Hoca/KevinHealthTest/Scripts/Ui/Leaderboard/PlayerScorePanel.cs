using UnityEngine;
using TMPro;

public class PlayerScorePanel : MonoBehaviour
{
    //only used for UI reference, update will be handled by PlayerTotalScoreDisplay
    [Header("Score Texts")]
    public TMP_Text roundText1;
    public TMP_Text roundText2;
    public TMP_Text roundText3;
    public TMP_Text totalScore; 
}
