using UnityEngine;

public class Score : MonoBehaviour
{
	public string uid;
	public double score;

	public Score(string uid_, double score_)
	{ 
		uid = uid_;
		score = score_;
	}
	
	public int Comparison(Score t)
	{
		if (score >= t.score)
			return 1;
		else
			return -1;
	}
	
}
