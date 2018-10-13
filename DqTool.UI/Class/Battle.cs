using System;
using System.Collections.Generic;
using System.Linq;
using DqTool.Core;

namespace DqTool.UI.Class
{
    public class Battle
    {
        private List<Monster> _monsters = new List<Monster>();

        //private void Clear()
        //{
        //    _monsters.ForEach(x => x.Dispose());
        //    _monsters.Clear();
        //}

        //private void CreateMonster(MonsterName name)
        //{

        //    _monsters.Add(new Monster(name));

        //    if (name == MonsterName.GenjinA)
        //    {
        //        _monsters.Add(new Monster(MonsterName.GenjinB));
        //        _monsters.Add(new Monster(MonsterName.GenjinC));
        //    }
        //    else if (name == MonsterName.BattlerA)
        //    {
        //        _monsters.Add(new Monster(MonsterName.BattlerB));
        //    }
        //}

        //private void CreateMonsters(IEnumerable<MonsterName> names)
        //{
        //    foreach (var name in names)
        //    {
        //        _monsters.Add(new Monster(name));
        //    }
        //}


        //internal void Update(GameStatus status)
        //{
        //    if (status.IsBattleStart)
        //    {
        //        Clear();
        //        CreateMonster(status.name);
        //    }
        //    else
        //    {
        //        Damage(status.name, status.Point);
        //    }
        //}

        //private void Damage(MonsterName name, int point)
        //{
        //    _monsters.FirstOrDefault(x => x.Name == name)?.Damage(point);
        //}
    }
}
