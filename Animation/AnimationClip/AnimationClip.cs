using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;

public class AnimationClip
{
    enum PlayMode { Play, Pause };

    Rectangle[] srcRects;
	PlayMode playMode = PlayMode.Play;
	float animTime = 0.0f;
	float speed = 6.0f;

    public AnimationClip(Rectangle[] srcRects)
	{
		this.srcRects = srcRects;
	}

	public void Play()
	{
		playMode = PlayMode.Play;
	}

    public void Pause()
    {
        playMode = PlayMode.Pause;
    }

	public void Update(float dt)
	{
		if (playMode == PlayMode.Pause) return;

		animTime += dt * speed;
	}

	public Rectangle GetCurrentSourceRectangle()
	{
		int rect_index = (int)animTime % srcRects.Length;
		return srcRects[rect_index];
	}
}
