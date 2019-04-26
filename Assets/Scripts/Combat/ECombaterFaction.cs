using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECombaterFaction
{
    Citizen, TheBrave, DungeonCitizen, TheDark
}

// this should be improved later
// one always see him/herself as ally
public static class ECombaterFactionExtention
{
    public static ECombaterRelationship GetRelationship(this ECombaterFaction self, ECombaterFaction other)
    {
        switch (self)
        {
            case ECombaterFaction.Citizen:
                switch (other)
                {
                    case ECombaterFaction.Citizen:
                        return ECombaterRelationship.Ally;
                    case ECombaterFaction.TheBrave:
                        return ECombaterRelationship.Ally;
                    case ECombaterFaction.DungeonCitizen:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.TheDark:
                        return ECombaterRelationship.Enemy;
                }
                break;
            case ECombaterFaction.TheBrave:
                switch (other)
                {
                    case ECombaterFaction.Citizen:
                        return ECombaterRelationship.Ally;
                    case ECombaterFaction.TheBrave:
                        return ECombaterRelationship.Ally;
                    case ECombaterFaction.DungeonCitizen:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.TheDark:
                        return ECombaterRelationship.Enemy;
                }
                break;
            case ECombaterFaction.DungeonCitizen:
                switch (other)
                {
                    case ECombaterFaction.Citizen:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.TheBrave:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.DungeonCitizen:
                        return ECombaterRelationship.Ally;
                    case ECombaterFaction.TheDark:
                        return ECombaterRelationship.Enemy;
                }
                break;
            case ECombaterFaction.TheDark:
                switch (other)
                {
                    case ECombaterFaction.Citizen:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.TheBrave:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.DungeonCitizen:
                        return ECombaterRelationship.Enemy;
                    case ECombaterFaction.TheDark:
                        return ECombaterRelationship.Ally;
                }
                break;
        }
        return ECombaterRelationship.Enemy;
    }


}