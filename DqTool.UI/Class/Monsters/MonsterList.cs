using System;
using System.Collections.Generic;
using System.Linq;
using DqTool.Core;

namespace DqTool.UI.Class.Monsters
{
    public static class MonsterList
    {
        private static readonly IEnumerable<MonsterBreed> _monsters = Serializer<List<MonsterBreed>>.Deserialize("Resources\\Monsters.xml");

        public static MonsterBreed GetBreed(MonsterName name) => _monsters.FirstOrDefault(x => x.Name == name);
    }
}
