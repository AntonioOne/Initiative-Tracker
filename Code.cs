using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace Initiative_Tracker
{
    public class Abilities
    {
        public int Strength = 0;
        public int Dexterity = 0;
        public int Constitution = 0;
        public int Intelligence = 0;
        public int Wisdom = 0;
        public int Charisma = 0;
    }

    public class Skills
    {
        public int Athletics = 0;

        public int Acrobatics = 0;
        public int SleightOfHand = 0;
        public int Stealth = 0;


        public int Arcana = 0;
        public int History = 0;
        public int Investigation = 0;
        public int Nature = 0;
        public int Religion = 0;

        public int AnimalHandling = 0;
        public int Insight = 0;
        public int Medicine = 0;
        public int Perception = 0;
        public int Survival = 0;

        public int Deception = 0;
        public int Intimidation = 0;
        public int Performance = 0;
        public int Persuasion = 0;

        public Skills(Abilities abs)
        {
            Athletics = abs.Strength;
            Arcana = History = Investigation = Nature = Religion = abs.Intelligence;
            AnimalHandling = Insight = Medicine = Perception = Survival = abs.Wisdom;
            Deception = Intimidation = Performance = Persuasion = abs.Charisma;
        }
    }

    public enum DamageType
    {
        Acid, Bludgeoning, Cold, Fire, Force, Lightning, Necrotic, Piercing, Poison, Psychic, Radiant, Slashing, Thunder, Raw
    }

    public class Damage
    {
        public DamageType damageType;
        public int damage;
    }

    static class EnumToString
    {
        private static string[] _damageTypeStr = { "Acid", "Bludgeoning", "Cold", "Fire", "Force", "Lightning", "Necrotic", "Piercing", "Poison", "Psychic", "Radiant", "Slashing", "Thunder", "Raw" };
        private static string[] _conditionStr = { "Blinded", "Charmed", "Deafened", "Fatigued", "Frightened", "Grappled", "Incapacitated", "Invisible", "Paralyzed", "Petrified", "Poisoned", "Prone", "Restrained", "Stunned", "Unconscious", "Exhaustion" };
        private static string[] _speedTypeStr = { "Walking", "Climbing", "Swimming", "Flying", "Burrow" };
        public static string GetDamageType(DamageType ind)
        {
            return _damageTypeStr[(int)ind];
        }

        public static string GetCondition(Condition ind)
        {
            return _conditionStr[(int)ind];
        }
    }

    public enum Condition
    {
        Blinded, Charmed, Deafened, Fatigued, Frightened, Grappled, Incapacitated, Invisible, Paralyzed, Petrified, Poisoned, Prone, Restrained, Stunned, Unconscious, Exhaustion
    }

    public enum LimitType
    {
        UsesPerDay, Recharge
    }

    public enum AttackType
    {
        Melee, Ranged, Mixed
    }

    public enum MovementType
    {
        Walking, Climbing, Swimming, Flying, Burrow
    }

    public class Speed
    {
        MovementType type;
        public int speed;
    }

    public class Action
    {
        public string Name;
        public string Text;
        public bool isLimited;
        public LimitType limitType;
        public int usesPerDay;
        public int rechargeLimit;
        public bool isAttack;
        public int meleeHitModifier;
        public int rangedHitModifier;
        public AttackType attackType;
        public string damage;
        public string additional;
        public bool isAOE;
        public bool isCheckRequiered;
        public int saveDC;
    }

    public class LegendaryAction : Action
    {
        public int cost;
    }

    public class Feature
    {
        public string Name;
        public string Text;
        public bool isLimited;
        public LimitType limitType;
        public int usesPerDay;
        public int rechargeLimit;
    }

    public class Creature : INotifyPropertyChanged
    {
        private bool _turn;
        public bool turn
        {
            get { return _turn; }
            set
            {
                _turn = value;
                NotifyPropertyChanged("turn");
            }
        }
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("name");
            }
        }
        public string type { get; set; }
        private string _label;
        public string label
        {
            get { return _label; }
            set
            {
                _label = value;
                NotifyPropertyChanged("label");
            }
        }
        public Abilities abilities { get; set; }
        public Skills skills { get; set; }
        public Abilities saves { get; set; }
        public HashSet<DamageType> immunities;
        public HashSet<DamageType> resistances;
        public HashSet<DamageType> vulnurabilities;
        private int _tempHP;
        public int tempHP
        {
            get { return _tempHP; }
            set
            {
                _tempHP = value;
                NotifyPropertyChanged("tempHP");
            }
        }
        private int _HP;
        public int HP
        {
            get { return _HP; }
            set
            {
                _HP = value;
                NotifyPropertyChanged("HP");
            }
        }
        private int _maxHP;
        public int maxHP
        {
            get { return _maxHP; }
            set
            {
                _maxHP = value;
                NotifyPropertyChanged("maxHP");
            }
        }
        private int _AC;
        public int AC
        {
            get { return _AC; }
            set
            {
                _AC = value;
                NotifyPropertyChanged("AC");
            }
        }
        private int _AC_mod;
        public int AC_Mod
        {
            get { return _AC_mod; }
            set
            {
                _AC_mod = value;
                NotifyPropertyChanged("AC_mod");
            }
        }
        private string _ArmorType;
        public string ArmorType
        {
            get { return _ArmorType; }
            set
            {
                _ArmorType = value;
                NotifyPropertyChanged("ArmorType");
            }
        }
        public HashSet<Condition> conditionImmunities;
        public HashSet<Condition> conditions;
        public List<string> senses;
        public List<string> languages;
        public List<Speed> movement;
        public List<Action> actions;
        public List<Feature> features;
        public List<LegendaryAction> legendaryActions;
        public event PropertyChangedEventHandler PropertyChanged;
        private int[] _SpellSlots;
        public int[] SpellSlots
        {
            get { return _SpellSlots; }
            set
            {
                _SpellSlots = value;
                NotifyPropertyChanged("SpellSlots");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public Creature()
        {
            turn = false;
            name = "Random name";
            HP = 50;
            maxHP = 50;
            AC = 15;
            type = "";
            label = "";
            abilities = new Abilities();
            skills = new Skills(abilities);
            saves = new Abilities();
            immunities = new HashSet<DamageType>();
            resistances = new HashSet<DamageType>();
            vulnurabilities = new HashSet<DamageType>();
            tempHP = 0;
            AC_Mod = 0;
            ArmorType = "none";
            conditionImmunities = new HashSet<Condition>();
            conditions = new HashSet<Condition>();
            senses = new List<string>();
            languages = new List<string>();
            movement = new List<Speed>();
            actions = new List<Action>();
            features = new List<Feature>();
            legendaryActions = new List<LegendaryAction>();
            SpellSlots = new int[8];
        }

        private void _ReduceHP(int val)
        {
            if (val >= tempHP)
            {
                val -= tempHP;
                tempHP = 0;
            }
            else
            {
                tempHP -= val;
                val = 0;
            }
            HP -= val;
        }
        public void DealDamage(List<Damage> damage)
        {
            foreach (var dmg in damage)
            {
                if (immunities.Contains(dmg.damageType))
                    continue;
                else if (resistances.Contains(dmg.damageType))
                {
                    _ReduceHP(dmg.damage / 2);
                }
                else
                {
                    _ReduceHP(dmg.damage);
                }
            }
        }

        public void DealDamage(Damage damage)
        {
            if (immunities.Contains(damage.damageType))
                return;
            else if (resistances.Contains(damage.damageType))
            {
                _ReduceHP(damage.damage / 2);
            }
            else
            {
                _ReduceHP(damage.damage);
            }
        }
    }

    public class GreaterThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                parameter = 0;
            int _parameter;
            if (int.TryParse(parameter.ToString(), out _parameter))
                return ((int)value) > _parameter;
            else
                throw new Exception("Wrong parameter for GreaterThanConverter: " + parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                parameter = 0;
            int _parameter;
            if (int.TryParse(parameter.ToString(), out _parameter))
                return ((int)value) < _parameter;
            else
                throw new Exception("Wrong parameter for LessThanConverter: " + parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public int Value { get; set; }
    }
}
