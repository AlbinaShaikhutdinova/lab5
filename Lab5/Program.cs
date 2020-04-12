using System;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            var session = new VPlayer(new Playing(),50);
            session.VolDown();
            session.PlayButton();
            session.PlayButton();
            session.VolUp();
            session.SleepButton();
            session.VolDown();
            Console.ReadLine();
        }
    }
    /// <summary>
    /// Интерфейс для состояний видеоплеера.
    /// </summary>
    abstract class State
    {
        protected VPlayer context;
        public int volume;
        /// <summary>
        /// Установление контекста
        /// </summary>
        /// <param name="context"> Состояние плеера </param>
        /// <param name="volume"> Громкость </param>
        public void SetContext(VPlayer context, int volume)
        {
            this.context = context;
            this.volume = volume;
        }
        /// <summary>
        /// Нажатие кнопки play
        /// </summary>
        public abstract void PlayButton();
        /// <summary>
        /// Нажатие кнопки sleep
        /// </summary>
        public abstract void SleepButton();
        /// <summary>
        /// Увеличение звука
        /// </summary>
        public abstract void VolUp();
        /// <summary>
        /// Уменьшение звука
        /// </summary>
        public abstract void VolDown();
    }
    /// <summary>
    /// Плеер воспроизводит видео
    /// </summary>
    class Playing : State
    {
        /// <summary>
        /// Нажатие кнопки play. Воспроизведение ставится на паузу
        /// </summary>
        public override void PlayButton()
        {
            Console.WriteLine("The video has been paused");
            this.context.TransitionTo(new Paused(),volume);
        }
        /// <summary>
        /// Нажатие кнопки sleep. Переход в состояние сна
        /// </summary>
        public override void SleepButton()
        {
            Console.WriteLine("The player has been put to sleeping mode");
            this.context.TransitionTo(new Sleeping(),volume);
        }
        /// <summary>
        /// Увеличение звука
        /// </summary>
        /// <param name="v"> Значение, на которое увеличиваем громкость. </param>
        /// <param name="volume"> Значение громкости. </param>
        public override void VolUp()
        {
            Console.Write("Increase volume by:");
            string vol=Console.ReadLine();
            try
            {
                byte v =Convert.ToByte(vol);
                if (volume + v > 100)
                    volume = 100;
                else
                    volume += v;
            }
            catch
            {
                Console.WriteLine("Wrong volume format");
            }
            
            Console.WriteLine("The volume is {0}",volume);
            
        }
        /// <summary>
        /// Увеличение звука
        /// </summary>
        /// <param name="v"> Значение, на которое уменьшаем громкость. </param>
        /// <param name="volume"> Значение громкости. </param>
        public override void VolDown()
        {
            Console.Write("Decrease volume by:");
            string vol = Console.ReadLine();
            try
            {
                byte v = Convert.ToByte(vol);
                if (volume - v < 0)
                    volume = 0;
                else
                    volume-= v;
            }
            catch
            {
                Console.WriteLine("Wrong volume format");
            }

            Console.WriteLine("The volume is {0}", volume);

        }
    }
    /// <summary>
    /// Видео остановлено
    /// </summary>
    class Paused : State
    {
        /// <summary>
        /// Нажатие кнопки play. Видео включается
        /// </summary>
        public override void PlayButton()
        {
            Console.WriteLine("The video is playing");
            this.context.TransitionTo(new Playing(),volume);
        }
        /// <summary>
        /// Нажатие кнопки sleep. Переход в состояние сна
        /// </summary>
        public override void SleepButton()
        {
            Console.WriteLine("The player has been put to sleeping mode");
            this.context.TransitionTo(new Sleeping(),volume);
        }
        /// <summary>
        /// Увеличение звука
        /// </summary>
        /// <param name="v"> Значение, на которое увеличиваем громкость. </param>
        /// <param name="volume"> Значение громкости. </param>
        public override void VolUp()
        {
            Console.Write("Increase volume by:");
            string vol = Console.ReadLine();
            try
            {
                byte v = Convert.ToByte(vol);
                if (volume + v > 100)
                    volume = 100;
                else
                    volume += v;
            }
            catch
            {
                Console.WriteLine("Wrong volume format");
            }

            Console.WriteLine("The volume is {0}", volume);

        }
        /// <summary>
        /// Увеличение звука
        /// </summary>
        /// <param name="v"> Значение, на которое уменьшаем громкость. </param>
        /// <param name="volume"> Значение громкости. </param>
        public override void VolDown()
        {
            Console.Write("Decrease volume by:");
            string vol = Console.ReadLine();
            try
            {
                byte v = Convert.ToByte(vol);
                if (volume - v < 0)
                    volume = 0;
                else
                    volume -= v;
            }
            catch
            {
                Console.WriteLine("Wrong volume format");
            }
            Console.WriteLine("The volume is {0}", volume);

        }
    }
    /// <summary>
    /// Плеер в состоянии сна
    /// </summary>
    class Sleeping : State
    {
        /// <summary>
        /// Нажатие кнопки play. Видео не включается
        /// </summary>
        public override void PlayButton()
        {
            Console.WriteLine("The player is in the sleeping mode");
            
        }
        /// <summary>
        /// Нажатие кнопки sleep. Выход из состояние сна 
        /// </summary>
        public override void SleepButton()
        {
            Console.WriteLine("The player is out of sleeping mode");
            this.context.TransitionTo(new Paused(),volume);
        }
        /// <summary>
        /// Плеер в состоянии сна. Никаких действий не предпринимается
        /// </summary>
        public override void VolUp()
        {
            Console.WriteLine("The player is in the sleeping mode");

        }
        /// <summary>
        /// Плеер в состоянии сна. Никаких действий не предпринимается
        /// </summary>
        public override void VolDown()
        {
            Console.WriteLine("The player is in the sleeping mode");

        }
    }

    /// <summary>
    /// Видеоплеер
    /// </summary>
    class VPlayer
    {
        /// <summary>
        /// Ссылка на текущее состояние плеера.
        /// </summary>
        public State _state = null;
        //public int volume;
        /// <summary>
        /// Изменение состояния плеера.
        /// </summary>
        public VPlayer(State state, int volume)
        {
            this.TransitionTo(state, volume);
        }
        /// <summary>
        /// Плеер позволяет изменять свое состояние и громкость во время выполнения.
        /// </summary>
        /// <param name="state"> Состояние плеера </param>
        /// /// <param name="volume"> Громкость плеера </param>
        public void TransitionTo(State state,int volume)
        {
            _state = state;
            _state.SetContext(this, volume);
        }

        // Контекст делегирует часть своего поведения текущему объекту
        // Состояния.
        public void PlayButton()
        {
            this._state.PlayButton();
        }
        public void SleepButton()
        {
            this._state.SleepButton();
        }
        public void VolUp()
        {
            this._state.VolUp();
        }
        public void VolDown()
        {
            this._state.VolDown();
        }

    }
}
