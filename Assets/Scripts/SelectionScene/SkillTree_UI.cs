using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree_UI : MonoBehaviour
{
    SkillTree skillTree;

    public Button ActiveSkillButton_0;
    public Button ActiveSkillButton_1;
    public Button ActiveSkillButton_2;
    public Button ActiveSkillButton_3;
    public Button ActiveSkillButton_4;
    public Button ActiveSkillButton_5;
    public Button ActiveSkillButton_6;
    public Button ActiveSkillButton_7;
    public Button ActiveSkillButton_8;
    public Button PassiveSkillButton_0;
    public Button PassiveSkillButton_1;
    public Button PassiveSkillButton_2;
    public Button PassiveSkillButton_3;
    public Button PassiveSkillButton_4;
    public Button PassiveSkillButton_5;
    public Button PassiveSkillButton_6;
    public Button PassiveSkillButton_7;
    public Button PassiveSkillButton_8;

    public GameObject infoPanel;
    TextMeshProUGUI skillName;
    TextMeshProUGUI skillDescribtion;
    Button learnButton;
    Button cancelButton;
    void Awake()
    {
        skillTree = FindObjectOfType<GameManager>().GetSkillTree();
        
        
        
        skillName = infoPanel.transform.Find("Skill_Name").GetComponent<TextMeshProUGUI>();
        skillDescribtion = infoPanel.transform.Find("Describtion").GetComponent<TextMeshProUGUI>();
        
        ActiveSkillButton_0.onClick.AddListener(Active0);
        ActiveSkillButton_1.onClick.AddListener(Active1);
        ActiveSkillButton_2.onClick.AddListener(Active2);
        ActiveSkillButton_3.onClick.AddListener(Active3);
        ActiveSkillButton_4.onClick.AddListener(Active4);
        ActiveSkillButton_5.onClick.AddListener(Active5);
        ActiveSkillButton_6.onClick.AddListener(Active6);
        ActiveSkillButton_7.onClick.AddListener(Active7);
        ActiveSkillButton_8.onClick.AddListener(Active8);
        PassiveSkillButton_0.onClick.AddListener(Passive0);
        PassiveSkillButton_1.onClick.AddListener(Passive1);
        PassiveSkillButton_2.onClick.AddListener(Passive2);
        PassiveSkillButton_3.onClick.AddListener(Passive3);
        PassiveSkillButton_4.onClick.AddListener(Passive4);
        PassiveSkillButton_5.onClick.AddListener(Passive5);
        PassiveSkillButton_6.onClick.AddListener(Passive6);
        PassiveSkillButton_7.onClick.AddListener(Passive7);
        PassiveSkillButton_8.onClick.AddListener(Passive8);
        SetColorsOnButtons();

        skillTree.onAnySkillUnlocked += SetButtonColors;
    }
    private void Start()
    {
        learnButton = infoPanel.transform.Find("Learn").GetComponent<Button>();

        cancelButton = infoPanel.transform.Find("Cancel").GetComponent<Button>();
        cancelButton.onClick.AddListener(Cancel);
    }

    private void SetButtonColors(object sender, SkillTree.OnAnySkillUnlocked e)
    {
        switch (e.skillType)
        {
            case SkillTree.SkillType.ActiveSkill_0:
                if(ActiveSkillButton_0 != null)
                {
                ActiveSkillButton_0.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                ActiveSkillButton_0.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);
                }

                break;
            case SkillTree.SkillType.ActiveSkill_1:
                if(ActiveSkillButton_1 != null)
                {
                ActiveSkillButton_1.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                ActiveSkillButton_1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);
                }
                
                break;
            case SkillTree.SkillType.ActiveSkill_2:
                if (ActiveSkillButton_3 != null)
                {
                ActiveSkillButton_2.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                ActiveSkillButton_2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.ActiveSkill_3:
                if (ActiveSkillButton_3!= null)
                {
                ActiveSkillButton_3.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                ActiveSkillButton_3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.ActiveSkill_4:
                if (ActiveSkillButton_4 != null) { 
                ActiveSkillButton_4.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                ActiveSkillButton_4.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);
                }
                break;
            case SkillTree.SkillType.ActiveSkill_5:
                if (ActiveSkillButton_5!= null)
                {
                ActiveSkillButton_5.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                ActiveSkillButton_5.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.ActiveSkill_6:
                if (ActiveSkillButton_6 != null)
                {
                    ActiveSkillButton_6.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    ActiveSkillButton_6.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f);

                }
                break;
            case SkillTree.SkillType.ActiveSkill_7:
                if (ActiveSkillButton_7 != null)
                {
                    ActiveSkillButton_7.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    ActiveSkillButton_7.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.ActiveSkill_8:
                if (ActiveSkillButton_8 != null)
                {
                    ActiveSkillButton_8.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    ActiveSkillButton_8.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_0:
                if (PassiveSkillButton_0!= null)
                {
                PassiveSkillButton_0.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_0.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_1:
                if (PassiveSkillButton_1 != null)
                {
                PassiveSkillButton_1.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_2:
                if (PassiveSkillButton_2!= null)
                {
                PassiveSkillButton_2.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_3:
                if (PassiveSkillButton_3!= null)
                {
                PassiveSkillButton_3.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_4:
                if (PassiveSkillButton_4!= null)
                {
                PassiveSkillButton_4.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_4.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_5:
                if (PassiveSkillButton_5!= null)
                {
                PassiveSkillButton_5.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_5.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_6:
                if (PassiveSkillButton_6!= null)
                {
                PassiveSkillButton_6.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_6.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_7:
                if(PassiveSkillButton_7 != null)
                {
                PassiveSkillButton_7.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_7.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
            case SkillTree.SkillType.PassiveSkill_8:
                if (PassiveSkillButton_8)
                {
                PassiveSkillButton_8.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                PassiveSkillButton_8.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = new Color(1f,1f,1f);

                }
                break;
                    
        }
        SetColorsOnButtons();
    }

    private void Active0()
    {
        if(learnButton != null)
        {
            learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive0);
        }
       
        
        var txt = ActiveSkillButton_0.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        
        infoPanel.SetActive(true);

        

    }

    private void LearnActive0()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_0);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }

    private void Active1()
    {

        if (learnButton != null)
        {

            learnButton.onClick.RemoveAllListeners();
        learnButton.onClick.AddListener(LearnActive1);
        }
    
        var txt = ActiveSkillButton_1.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);

        infoPanel.SetActive(true);

    }
    private void LearnActive1()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_1);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active2()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
        learnButton.onClick.AddListener(LearnActive2);
    }
        var txt = ActiveSkillButton_2.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

    }
    private void LearnActive2()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_2);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active3()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive3);
        }
        var txt = ActiveSkillButton_3.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

        
    }
    private void LearnActive3()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_3);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active4()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive4);
        }
        var txt = ActiveSkillButton_4.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

        
    }
    private void LearnActive4()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_4);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active5()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive5);
        }
        var txt = ActiveSkillButton_5.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

       
    }
    private void LearnActive5()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_5);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active6()
    {
        if (learnButton != null)
        {

            learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive6);
        }
        var txt = ActiveSkillButton_6.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);


    }
    private void LearnActive6()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_6);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active7()
    {
        if (learnButton != null)
        {

            learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive7);
        }
        var txt = ActiveSkillButton_7.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);


    }
    private void LearnActive7()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_7);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Active8()
    {
        if (learnButton != null)
        {

            learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnActive8);
        }
        var txt = ActiveSkillButton_8.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);


    }
    private void LearnActive8()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.ActiveSkill_8);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive0()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive0);
        }
        var txt = PassiveSkillButton_0.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

        

    }
    private void LearnPassive0()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_0);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive1()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive1);
        }
        var txt = PassiveSkillButton_1.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

        

    }
    private void LearnPassive1()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_1);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive2()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive2);
        }
        var txt = PassiveSkillButton_2.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

       

    }
    private void LearnPassive2()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_2);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive3()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive3);
        }
        var txt = PassiveSkillButton_3.GetComponent<SkillDescribtion>();

        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

       

    }
    private void LearnPassive3()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_3);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive4()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive4);
        }
        var txt = PassiveSkillButton_4.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);

        infoPanel.SetActive(true);

       

    }
    private void LearnPassive4()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_4);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive5()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive5);
        }
        var txt = PassiveSkillButton_5.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

       

    }
    private void LearnPassive5()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_5);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive6()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive6);
        }
        var txt = PassiveSkillButton_6.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

       

    }
    private void LearnPassive6()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_6);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive7()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive7);
        }
        var txt = PassiveSkillButton_7.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

        

    }
    private void LearnPassive7()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_7);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }
    private void Passive8()
    {
        if(learnButton != null){

        learnButton.onClick.RemoveAllListeners();
            learnButton.onClick.AddListener(LearnPassive8);
        }
        var txt = PassiveSkillButton_8.GetComponent<SkillDescribtion>();
        SetInfoPanelText(txt.skillName, txt.skillDescribtion);
        infoPanel.SetActive(true);

       

    }
    private void LearnPassive8()
    {
        skillTree.TryUnlockingSkill(SkillTree.SkillType.PassiveSkill_8);
        infoPanel.SetActive(false);
        learnButton.onClick.RemoveAllListeners();
    }


    public void SetSkillTree(SkillTree skillTree)
    {
        this.skillTree = skillTree;
    }

    public void SetInfoPanelText(string name, string desc)
    {
        skillName.text = name;
        skillDescribtion.text = desc;
    }

    public void Cancel()
    {
        learnButton.onClick.RemoveAllListeners();
        infoPanel.SetActive(false);

    }

    //Colors

    void SetColorsOnButtons()
    {
       if(ActiveSkillButton_0 != null)
        {
            var A0 = ActiveSkillButton_0.GetComponent<Image>();
            var A0t = ActiveSkillButton_0.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_0))
            {
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_0))
                {
                    A0.color = new Color(0.8f, 0.8f, 0.8f);
                    A0t.color = new Color(0.4f, 0.4f, 0.4f);

                }
                else
                {

                    A0.color = new Color(0.1f, 0.1f, 0.1f);
                    A0t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else {
                A0.color = new Color(1f, 1f, 1f);
                A0t.color = new Color(1f, 1f, 1f);
            }
            
        }

        if (ActiveSkillButton_1 != null)
        {
            var A1 = ActiveSkillButton_1.GetComponent<Image>();
            var A1t = ActiveSkillButton_1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_1))
            {
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_1))
                {
                    A1.color = new Color(0.8f, 0.8f, 0.8f);
                    A1t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A1.color = new Color(0.1f, 0.1f, 0.1f);
                    A1t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                A1.color = new Color(1f, 1f, 1f);
                A1t.color = new Color(1f, 1f, 1f);
            }
        }
        if (ActiveSkillButton_2 != null)
        {
            var A2 = ActiveSkillButton_2.GetComponent<Image>();
            var A2t = ActiveSkillButton_2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_2)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_2))
                {
                    A2.color = new Color(0.8f, 0.8f, 0.8f);
                    A2t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A2.color = new Color(0.1f, 0.1f, 0.1f);
                    A2t.color = new Color(0.3f, 0.3f, 0.3f);
                }
        }
        else
        {
            A2.color = new Color(1f, 1f, 1f);
            A2t.color = new Color(1f, 1f, 1f);
        }
    }
        if (ActiveSkillButton_3 != null)
        {
            var A3 = ActiveSkillButton_3.GetComponent<Image>();
            var A3t = ActiveSkillButton_3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_3)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_3))
                {
                    A3.color = new Color(0.8f, 0.8f, 0.8f);
                    A3t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A3.color = new Color(0.1f, 0.1f, 0.1f);
                    A3t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                A3.color = new Color(1f, 1f, 1f);
                A3t.color = new Color(1f, 1f, 1f);
            }
        }
        if (ActiveSkillButton_4 != null)
        {
            var A4 = ActiveSkillButton_4.GetComponent<Image>();
            var A4t = ActiveSkillButton_4.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_4)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_4))
                {
                    A4.color = new Color(0.8f, 0.8f, 0.8f);
                    A4t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A4.color = new Color(0.1f, 0.1f, 0.1f);
                    A4t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                A4.color = new Color(1f, 1f, 1f);
                A4t.color = new Color(1f, 1f, 1f);
            }
        }
        if (ActiveSkillButton_5 != null)
        {
            var A5 = ActiveSkillButton_5.GetComponent<Image>();
            var A5t = ActiveSkillButton_5.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_5)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_5))
                {
                    A5.color = new Color(0.8f, 0.8f, 0.8f);
                    A5t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A5.color = new Color(0.1f, 0.1f, 0.1f);
                    A5t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                A5.color = new Color(1f, 1f, 1f);
                A5t.color = new Color(1f, 1f, 1f);
            }
        }
        if (ActiveSkillButton_6 != null)
        {
            var A6 = ActiveSkillButton_6.GetComponent<Image>();
            var A6t = ActiveSkillButton_6.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_6)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_6))
                {
                    A6.color = new Color(0.8f, 0.8f, 0.8f);
                    A6t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A6.color = new Color(0.1f, 0.1f, 0.1f);
                    A6t.color = new Color(0.3f, 0.3f, 0.3f);
                }
        }
        else
        {
            A6.color = new Color(1f, 1f, 1f);
            A6t.color = new Color(1f, 1f, 1f);
        }
    }
        if (ActiveSkillButton_7 != null)
        {
            var A7 = ActiveSkillButton_7.GetComponent<Image>();
            var A7t = ActiveSkillButton_7.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_7)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_7))
                {
                    A7.color = new Color(0.8f, 0.8f, 0.8f);
                    A7t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A7.color = new Color(0.1f, 0.1f, 0.1f);
                    A7t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                A7.color = new Color(1f, 1f, 1f);
                A7t.color = new Color(1f, 1f, 1f);
            }
        }
        if (ActiveSkillButton_8 != null)
        {
            var A8 = ActiveSkillButton_8.GetComponent<Image>();
            var A8t = ActiveSkillButton_8.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.ActiveSkill_8)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.ActiveSkill_8))
                {
                    A8.color = new Color(0.8f, 0.8f, 0.8f);
                    A8t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    A8.color = new Color(0.1f, 0.1f, 0.1f);
                    A8t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                A8.color = new Color(1f, 1f, 1f);
                A8t.color = new Color(1f, 1f, 1f);
            }
        }
        //Passive
        if (PassiveSkillButton_0 != null)
        {
            var P0 = PassiveSkillButton_0.GetComponent<Image>();
            var P0t = PassiveSkillButton_0.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_0)) { 

                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_0)) 
                {
                    P0.color = new Color(0.8f, 0.8f, 0.8f);
                    P0t.color = new Color(0.4f, 0.4f, 0.4f);

                }
                else
                {

                    P0.color = new Color(0.1f, 0.1f, 0.1f);
                    P0t.color = new Color(0.3f, 0.3f, 0.3f);
                }
        }
        else
        {
            P0.color = new Color(1f, 1f, 1f);
            P0t.color = new Color(1f, 1f, 1f);
        }
    }

        if (PassiveSkillButton_1 != null)
        {
            var P1 = PassiveSkillButton_1.GetComponent<Image>();
            var P1t = PassiveSkillButton_1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_1)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_1))
                {
                    P1.color = new Color(0.8f, 0.8f, 0.8f);
                    P1t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P1.color = new Color(0.1f, 0.1f, 0.1f);
                    P1t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P1.color = new Color(1f, 1f, 1f);
                P1t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_2 != null)
        {
            var P2 = PassiveSkillButton_2.GetComponent<Image>();
            var P2t = PassiveSkillButton_2.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_2)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_2))
                {
                    P2.color = new Color(0.8f, 0.8f, 0.8f);
                    P2t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P2.color = new Color(0.1f, 0.1f, 0.1f);
                    P2t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P2.color = new Color(1f, 1f, 1f);
                P2t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_3 != null)
        {
            var P3 = PassiveSkillButton_3.GetComponent<Image>();
            var P3t = PassiveSkillButton_3.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_3)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_3))
                {
                    P3.color = new Color(0.8f, 0.8f, 0.8f);
                    P3t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P3.color = new Color(0.1f, 0.1f, 0.1f);
                    P3t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P3.color = new Color(1f, 1f, 1f);
                P3t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_4 != null)
        {
            var P4 = PassiveSkillButton_4.GetComponent<Image>();
            var P4t = PassiveSkillButton_4.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_4)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_4))
                {
                    P4.color = new Color(0.8f, 0.8f, 0.8f);
                    P4t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P4.color = new Color(0.1f, 0.1f, 0.1f);
                    P4t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P4.color = new Color(1f, 1f, 1f);
                P4t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_5 != null)
        {
            var P5 = PassiveSkillButton_5.GetComponent<Image>();
            var P5t = PassiveSkillButton_5.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_5)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_5))
                {
                    P5.color = new Color(0.8f, 0.8f, 0.8f);
                    P5t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P5.color = new Color(0.1f, 0.1f, 0.1f);
                    P5t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P5.color = new Color(1f, 1f, 1f);
                P5t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_6 != null)
        {
            var P6 = PassiveSkillButton_6.GetComponent<Image>();
            var P6t = PassiveSkillButton_6.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_6)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_6))
                {
                    P6.color = new Color(0.8f, 0.8f, 0.8f);
                    P6t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P6.color = new Color(0.1f, 0.1f, 0.1f);
                    P6t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P6.color = new Color(1f, 1f, 1f);
                P6t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_7 != null)
        {
            var P7 = PassiveSkillButton_7.GetComponent<Image>();
            var P7t = PassiveSkillButton_7.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_7)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_7))
                {
                    P7.color = new Color(0.8f, 0.8f, 0.8f);
                    P7t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P7.color = new Color(0.1f, 0.1f, 0.1f);
                    P7t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P7.color = new Color(1f, 1f, 1f);
                P7t.color = new Color(1f, 1f, 1f);
            }
        }
        if (PassiveSkillButton_8 != null)
        {
            var P8 = PassiveSkillButton_8.GetComponent<Image>();
            var P8t = PassiveSkillButton_8.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            if (!skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_8)) { 
                if (skillTree.canSkillBeUnlocked(SkillTree.SkillType.PassiveSkill_8))
                {
                    P8.color = new Color(0.8f, 0.8f, 0.8f);
                    P8t.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    P8.color = new Color(0.1f, 0.1f, 0.1f);
                    P8t.color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else
            {
                P8.color = new Color(1f, 1f, 1f);
                P8t.color = new Color(1f, 1f, 1f);
            }
        }
    }
}
