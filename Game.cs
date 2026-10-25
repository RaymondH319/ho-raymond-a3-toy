using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    private Ball ball = new Ball();
    private Paddle paddle = new Paddle();

    private Star[] stars = new Star[5];

    public void Setup()
    {
        Window.SetSize(800, 600);
        Window.SetTitle("Bouncing Ball Toy");

        ball.X = 400;
        ball.Y = 300;
        ball.SpeedX = 4;
        ball.SpeedY = -4;

        paddle.X = 325;
        paddle.Y = 550;
        paddle.Width = 150;
        paddle.Height = 20;
        paddle.Speed = 8;

        stars[0] = new Star(100, 100);
        stars[1] = new Star(200, 80);
        stars[2] = new Star(350, 140);
        stars[3] = new Star(600, 90);
        stars[4] = new Star(720, 120);
    }

    public void Update()
    {
        Window.ClearBackground(0);

        MovePaddle();
        MoveBall();
        CheckWallCollision();
        CheckPaddleCollision();

        DrawStars();
        DrawBall();
        DrawPaddle();
    }

    private void MovePaddle()
    {
        if (Input.IsKeyboardKeyDown(KeyboardKey.A))
        {
            paddle.X -= paddle.Speed;
        }

        if (Input.IsKeyboardKeyDown(KeyboardKey.D))
        {
            paddle.X += paddle.Speed;
        }

        if (paddle.X < 0)
        {
            paddle.X = 0;
        }

        if (paddle.X + paddle.Width > 800)
        {
            paddle.X = 800 - paddle.Width;
        }
    }

    private void MoveBall()
    {
        ball.X += ball.SpeedX;
        ball.Y += ball.SpeedY;
    }

    private void CheckWallCollision()
    {
        if (ball.X <= 15)
        {
            ball.SpeedX *= -1;
        }

        if (ball.X >= 785)
        {
            ball.SpeedX *= -1;
        }

        if (ball.Y <= 15)
        {
            ball.SpeedY *= -1;
        }

        if (ball.Y > 620)
        {
            ball.X = 400;
            ball.Y = 300;

            ball.SpeedX = 4;
            ball.SpeedY = -4;
        }
    }

    private void CheckPaddleCollision()
    {
        bool hitPaddle =
            ball.X > paddle.X &&
            ball.X < paddle.X + paddle.Width &&
            ball.Y + 15 > paddle.Y &&
            ball.Y - 15 < paddle.Y + paddle.Height;

        if (hitPaddle)
        {
            ball.SpeedY *= -1;
        }
    }

    private void DrawStars()
    {
        Draw.SetFillColor(255);

        for (int i = 0; i < stars.Length; i++)
        {
            Draw.Circle(stars[i].X, stars[i].Y, 3);
        }

        for (int i = 0; i < 800; i += 50)
        {
            Draw.Circle(i, 25, 2);
        }
    }

    private void DrawBall()
    {
        Draw.SetFillColor(255, 0, 0);
        Draw.Circle(ball.X, ball.Y, 15);
    }

    private void DrawPaddle()
    {
        Draw.SetFillColor(255);

        Draw.Rectangle(
            paddle.X,
            paddle.Y,
            paddle.Width,
            paddle.Height);
    }
}

class Ball
{
    public float X;
    public float Y;

    public float SpeedX;
    public float SpeedY;
}

class Paddle
{
    public float X;
    public float Y;

    public float Width;
    public float Height;

    public float Speed;
}

class Star
{
    public float X;
    public float Y;

    public Star(float x, float y)
    {
        X = x;
        Y = y;
    }
}