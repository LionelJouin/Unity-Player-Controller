namespace Assets.Scripts.Player
{
    public class Joystick
    {

        private int _playerNumber;

        public readonly string Horizontal;
        public readonly string Vertical;
        public readonly string Dash;
        public readonly string Action;

        public Joystick(int playerNumber)
        {
            _playerNumber = playerNumber;

            Horizontal = "Horizontal_P" + playerNumber;
            Vertical = "Vertical_P" + playerNumber;
            Dash = "Dash_P" + playerNumber;
            Action = "Action_P" + playerNumber;
        }
    }
}
