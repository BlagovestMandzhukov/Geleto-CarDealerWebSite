namespace GeletoCarDealer.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum Specifications
    {
        [Display(Name = "Климатик")]
        Aircon = 1,

        [Display(Name = "Климатроник")]
        Climatrinic = 2,

        [Display(Name = "Кожен салон")]
        LeatherSeats = 3,

        [Display(Name = "Ел.стъкла")]
        ElWindows = 4,

        [Display(Name = "Ел.огледала")]
        ElMirrors = 5,

        [Display(Name = "Ел.седалки")]
        ElSeats = 6,

        [Display(Name = "Подгряване на седалки")]
        HeatedSeats = 7,

        [Display(Name = "Шибедах")]
        Sunroof = 8,

        [Display(Name = "Стерео уредба")]
        Stereo = 9,

        [Display(Name = "Алуминиеви джанти")]
        AluminumWheels = 10,

        [Display(Name = "DVD/TV")]
        DVD = 11,

        [Display(Name = "Мултифунк. волан")]
        MultiFuncSteeringWheel = 12,

        [Display(Name = "4x4")]
        FourByFour = 13,

        [Display(Name = "ABS")]
        Abs = 14,

        [Display(Name = "ESP")]
        Esp = 15,

        [Display(Name = "Airbag")]
        Airbag = 16,

        [Display(Name = "Ксенонови фарове")]
        XenonHeadLight = 17,

        [Display(Name = "Халогенни фарове")]
        HalogenLights = 18,

        [Display(Name = "ASR/Тракшън контрол")]
        Asr = 19,

        [Display(Name = "Парктроник")]
        Parktronic = 20,

        [Display(Name = "Аларма")]
        Alarm = 21,

        [Display(Name = "Имобилайзер")]
        Immobilizer = 22,

        [Display(Name = "Центр. заключване")]
        CentralLocking = 23,

        [Display(Name = "Застраховка")]
        Ensurance = 24,

        [Display(Name = "Брониран")]
        Bulletproof = 25,

        [Display(Name = "Старт-Стоп система")]
        StartStop = 26,

        [Display(Name = "Безключово палене")]
        Keyless = 27,

        [Display(Name = "Типтроник/Мултитроник")]
        Tiptronic = 28,

        [Display(Name = "Автопилот")]
        Autopilot = 29,

        [Display(Name = "Серво управление")]
        PowerSteer = 30,

        [Display(Name = "Бордови компютър")]
        BoadComputer = 31,

        [Display(Name = "Сервизна книжка")]
        ServiceBook = 32,

        [Display(Name = "Гаранция")]
        Waranty = 33,

        [Display(Name = "Навигационна система")]
        Navigation = 34,

        [Display(Name = "Десен волан")]
        RightSteeringWheel = 35,

        [Display(Name = "Тунинг")]
        Tunning = 36,

        [Display(Name = "Панорамен покрив")]
        PanoramicRoof = 37,

        [Display(Name = "TAXI")]
        Taxi = 38,

        [Display(Name = "Ретро")]
        Retro = 39,

        [Display(Name = "Теглич")]
        Tow = 40,

        [Display(Name = "7 места (6+1)")]
        SevenSeats = 41,

        [Display(Name = "Хладилен")]
        Refrigerated = 42,
    }
}
