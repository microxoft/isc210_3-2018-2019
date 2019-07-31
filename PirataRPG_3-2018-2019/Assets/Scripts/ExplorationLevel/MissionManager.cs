using Assets.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    Mission newMission;
    MissionTask newMissionTask;
    TaskCondition newTaskCondition;
    TaskAction newTaskAction;
    List<MissionTask> tasksRemoved = new List<MissionTask>();
    List<Mission> missionsRemoved = new List<Mission>();
    const float MAXCLOSEDISTANCE = 1f;

    private void Update()
    {
        // Let's check the missions:
        foreach(Mission currentMission in GetNextMissions())
        {
            foreach(MissionTask currentTask in currentMission.Tasks)
            {
                if(ConditionsMet(currentTask))
                {
                    Debug.Log(currentTask.description);
                    ExecuteTaskActions(currentTask);
                    tasksRemoved.Add(currentTask);
                }
            }
            foreach (MissionTask currentTask in tasksRemoved)
                currentMission.Tasks.Remove(currentTask);
            if (currentMission.Tasks.Count == 0)
            {
                missionsRemoved.Add(currentMission);
                Debug.Log(currentMission.description);
            }
                
        }
        foreach (Mission currentMission in missionsRemoved)
            Game.Instance().CurrentLevel.Missions.Remove(currentMission);
    }

    public void LoadMissions(XmlDocument xmlDoc)
    {
        var selectedNodes = xmlDoc.SelectNodes("//level/missions/mission");

        foreach (XmlNode currentNode in selectedNodes) // For every mission...
        {
            newMission = new Mission { id = currentNode.Attributes["id"].Value,
                                    description = currentNode.Attributes["description"].Value,
                                    prerequisites = currentNode.Attributes["prerequisites"].Value
            };
            newMission.Tasks = new List<MissionTask>();

            var selectedTasks = xmlDoc.SelectNodes(string.Format("//level/missions/mission[@id='{0}']/tasks/task", newMission.id));
            foreach (XmlNode currentTask in selectedTasks)
            {
                newMissionTask = new MissionTask { id = currentTask.Attributes["id"].Value,
                                        description = currentNode.Attributes["description"].Value
                };
                newMissionTask.Conditions = new List<TaskCondition>();

                var selectedTaskConditions = xmlDoc.SelectNodes(string.Format("//level/missions/mission[@id='{0}']/tasks/task[@id='{1}']/conditions/condition", newMission.id, newMissionTask.id));
                foreach(XmlNode currentTaskCondition in selectedTaskConditions)
                {
                    newTaskCondition = new TaskCondition { Type = (TaskConditionType)Enum.Parse(typeof(TaskConditionType), currentTaskCondition.Attributes["type"].Value),
                        uniqueObjectNameFrom = currentTaskCondition.Attributes["uniqueObjectNameFrom"].Value,
                        uniqueObjectNameTo = currentTaskCondition.Attributes["uniqueObjectNameTo"].Value,
                        Quantity = Convert.ToSingle(currentTaskCondition.Attributes["quantity"].Value)
                    };
                    newMissionTask.Conditions.Add(newTaskCondition);
                }

                newMissionTask.Actions = new List<TaskAction>();
                var selectedTaskActions = xmlDoc.SelectNodes(string.Format("//level/missions/mission[@id='{0}']/tasks/task[@id='{1}']/actions/action", newMission.id, newMissionTask.id));
                foreach (XmlNode currentTaskAction in selectedTaskActions)
                {
                    newTaskAction = new TaskAction
                    {
                        Type = (TaskActionType)Enum.Parse(typeof(TaskActionType), currentTaskAction.Attributes["type"].Value),
                        uniqueObjectNameFrom = currentTaskAction.Attributes["uniqueObjectNameFrom"].Value,
                        uniqueObjectNameTo = currentTaskAction.Attributes["uniqueObjectNameTo"].Value,
                        Quantity = Convert.ToSingle(currentTaskAction.Attributes["quantity"].Value)
                    };
                    newMissionTask.Actions.Add(newTaskAction);
                }
                newMission.Tasks.Add(newMissionTask);
            }
            Game.Instance().CurrentLevel.Missions.Add(newMission);
        }
    }

    List<Mission> GetNextMissions()
    {
        return Game.Instance().CurrentLevel.Missions.Where(m => string.IsNullOrEmpty(m.prerequisites)).ToList();
    }

    void CheckPrerequisites(string missionId)
    {
        foreach(Mission currentMission in Game.Instance().CurrentLevel.Missions.Where(m => m.prerequisites.Split(',').Contains(missionId)))
        {
            List<string> newPrerequisites = currentMission.prerequisites.Split(',').ToList();
            newPrerequisites.Remove(missionId);
            currentMission.prerequisites = JoinPrerequisites(newPrerequisites);
        }
    }

    string JoinPrerequisites(List<string> prerequisites)
    {
        string str = string.Empty;

        foreach(string currentPrerequisite in prerequisites)
        {
            str += currentPrerequisite + ",";
        }
        str.Remove(str.Length - 1);
        return str;
    }

    bool ConditionsMet(MissionTask currentTask)
    {
        foreach(TaskCondition currentCondition in currentTask.Conditions)
        {
            switch(currentCondition.Type)
            {
                case TaskConditionType.CloseTo: // Cuidado con CaveEntrance
                    if (!IsCloseTo(
                        Game.Instance().CurrentLevel.Entities.FirstOrDefault(ent => ent.UniqueObjectName == currentCondition.uniqueObjectNameFrom).gameObject,
                        Game.Instance().CurrentLevel.Entities.FirstOrDefault(ent => ent.UniqueObjectName == currentCondition.uniqueObjectNameTo).gameObject))
                    {
                        Debug.Log("No está cerca." + currentCondition.uniqueObjectNameFrom + " de " + currentCondition.uniqueObjectNameTo);
                        return false;
                    }
                        
                    break;
                case TaskConditionType.Destroyed:
                    if (!IsDestroyed(currentCondition.uniqueObjectNameFrom))
                        return false;
                    break;
                case TaskConditionType.Inventoried:
                    if (!IsInventoried(currentCondition.uniqueObjectNameFrom, currentCondition.Quantity))
                        return false;
                    break;
                case TaskConditionType.KeyPressed:
                    if (!Input.GetButton(currentCondition.uniqueObjectNameFrom))
                        return false;
                    break;
            }
        }

        return true;
    }

    bool IsCloseTo(GameObject from, GameObject to)
    {
        return Vector3.Distance(from.transform.position, to.transform.position) <= MAXCLOSEDISTANCE;
    }

    bool IsDestroyed(string uniqueObjectName)
    {
        return !Game.Instance().CurrentLevel.Entities.Any(ent => ent.UniqueObjectName == uniqueObjectName);
    }

    bool IsInventoried(string prefabName, float quantity)
    {
        return Game.Instance().Inventory.Any(item => item.PrefabName == prefabName && item.Quantity >= quantity);
    }

    void ExecuteTaskActions(MissionTask currentTask)
    {
        foreach(TaskAction currentAction in currentTask.Actions)
        {
            switch(currentAction.Type)
            {
                case TaskActionType.InventoryAdd:
                    AddInventory(currentAction.uniqueObjectNameFrom, currentAction.Quantity);
                    break;
                case TaskActionType.LoadScene:
                    SceneManager.LoadScene(currentAction.uniqueObjectNameFrom);
                    break;
                case TaskActionType.ShowMessage:
                    Debug.Log(currentAction.uniqueObjectNameFrom);
                    break;
            }
        }
    }

    void AddInventory(string prefabName, float quantity)
    {
        InventoryItem item = Game.Instance().Inventory.Find(it => it.PrefabName == prefabName);

        if (item == null)
            Game.Instance().Inventory.Add(new InventoryItem { Name = prefabName, PrefabName = prefabName, Quantity = quantity });
        else
            item.Quantity += quantity;
    }
}
