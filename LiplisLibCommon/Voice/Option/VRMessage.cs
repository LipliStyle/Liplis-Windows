using Liplis.Msg;
using Liplis.Voice;
using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Liplis.Option.Voice
{ 
    public class VRMessage
    {
    public static string encodeTypeofMessage(string msg, msgVoiceRoid option)
    {
        string result = msg;
        switch (option.eCallType)
        {
            case ECallType.Nico:
                if (msg.Length >= 3)
                {
                    result = msg.Remove(0, 3);
                }
                break;
            case ECallType.Auto:
                if (msg.Length >= 3 && msg.Substring(0, 3) == "/W:")
                {
                    result = msg.Remove(0, 3);
                }
                break;
        }
        return result;
    }
    public static string replaceMessage(string msg, ref msgVoiceRoid option)
    {
        string text = msg;
        bool flag = false;
        if (option.bStudy)
        {
            if (flag = VRMessage.checkStudyCommand(msg))
            {
                text = VRMessage.commandStudy(msg, ref option);
            }
            else
            {
                flag = VRMessage.commandOblivion(ref text, ref option);
            }
        }
        if (!flag && option.bSelWord)
        {
            if (flag = VRMessage.checkSelWordAddCommand(text))
            {
                text = VRMessage.commandSelWordAdd(msg, ref option);
            }
            else
            {
                flag = VRMessage.commandSelWordOblivion(ref text, ref option);
            }
        }
        if (!flag)
        {
            text = VRMessage.replaceSimple(text, option);
            text = VRMessage.replaceReg(text, option);
        }
        else
        {
            
        }
        return text;
    }
    private static bool commandOblivion(ref string msg, ref msgVoiceRoid option)
    {
        string text = msg;
        if (text.IndexOf("忘却(") != 0)
        {
            return false;
        }
        if (text.LastIndexOf(")") != msg.Length - 1)
        {
            return false;
        }
        text = text.Remove(msg.Length - 1);
        string text2 = text.Substring(3);
        int num = 0;
        bool flag = false;
        IEnumerator enumerator = option.lstSimpleReplace.GetEnumerator();
        while (enumerator.MoveNext())
        {
            TSimpleReplace tSimpleReplace = (TSimpleReplace)enumerator.Current;
            if (tSimpleReplace.sRead == text2)
            {
                flag = true;
                break;
            }
            num++;
        }
        if (flag)
        {
            option.lstSimpleReplace.RemoveAt(num);
            msg = text2 + " を忘却しました。";
        }
        return flag;
    }
    private static bool commandSelWordOblivion(ref string msg, ref msgVoiceRoid option)
    {
        string text = msg;
        if (text.IndexOf("忘却(") != 0)
        {
            return false;
        }
        if (text.LastIndexOf(")") != msg.Length - 1)
        {
            return false;
        }
        text = text.Remove(msg.Length - 1);
        string text2 = text.Substring(3);
        int num = 0;
        bool flag = false;
        IEnumerator enumerator = option.lstSwitchKeyword.GetEnumerator();
        while (enumerator.MoveNext())
        {
            TSwitchKeyword tSwitchKeyword = (TSwitchKeyword)enumerator.Current;
            if (tSwitchKeyword.sKeyword == text2)
            {
                flag = true;
                break;
            }
            num++;
        }
        if (flag)
        {
            option.lstSwitchKeyword.RemoveAt(num);
            msg = text2 + " を振り分けキーワードから忘却しました。";
        }
        return flag;
    }
    private static string commandStudy(string msg, ref msgVoiceRoid option)
    {
        msg = msg.Remove(msg.Length - 1);
        int num = msg.IndexOf("=");
        string text = msg.Substring(3, num - 3);
        string text2 = msg.Substring(num + 1);
        bool flag = false;
        IEnumerator enumerator = option.lstSimpleReplace.GetEnumerator();
        while (enumerator.MoveNext())
        {
            TSimpleReplace tSimpleReplace = (TSimpleReplace)enumerator.Current;
            if (tSimpleReplace.sRead == text)
            {
                flag = true;
                tSimpleReplace.sReplace = text2;
                break;
            }
        }
        if (!flag)
        {
            TSimpleReplace item;
            item.sRead = text;
            item.sReplace = text2;
            item.bIgnore = true;
            option.lstSimpleReplace.Add(item);
        }
        return text + " は " + text2 + " と教育しました。";
    }
    private static bool checkStudyCommand(string msg)
    {
        if (msg.IndexOf("教育(") != 0)
        {
            return false;
        }
        if (msg.LastIndexOf(")") != msg.Length - 1)
        {
            return false;
        }
        int num = msg.IndexOf("=");
        return num >= 4 && num + 1 < msg.Length - 1;
    }
    private static string commandSelWordAdd(string msg, ref msgVoiceRoid option)
    {
        msg = msg.Remove(msg.Length - 1);
        int num = msg.IndexOf("+");
        bool flag = true;
        if (num < 0)
        {
            num = msg.IndexOf("*");
            flag = false;
        }
        string b = msg.Substring(3, num - 3);
        string text = msg.Substring(num + 1);
        bool flag2 = false;
        TVoiceRoidInfo tVoiceroid = default(TVoiceRoidInfo);
        IEnumerator enumerator = option.lstSwitchKeyword.GetEnumerator();
        while (enumerator.MoveNext())
        {
            TSwitchKeyword tSwitchKeyword = (TSwitchKeyword)enumerator.Current;
            if (tSwitchKeyword.sKeyword == b)
            {
                tVoiceroid = tSwitchKeyword.tVoiceroid;
                flag2 = true;
                break;
            }
        }
        if (flag2)
        {
            flag2 = false;
            int num2 = 0;
            IEnumerator enumerator2 = option.lstSwitchKeyword.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                TSwitchKeyword tSwitchKeyword2 = (TSwitchKeyword)enumerator2.Current;
                if (tSwitchKeyword2.sKeyword == text)
                {
                    flag2 = true;
                    break;
                }
                num2++;
            }
            if (flag2)
            {
                option.lstSwitchKeyword.RemoveAt(num2);
            }
            TSwitchKeyword item = default(TSwitchKeyword);
            item.bRead = flag;
            item.tVoiceroid = tVoiceroid;
            item.sKeyword = text;
            option.lstSwitchKeyword.Add(item);
        }
        if (!flag)
        {
            return "振り分けキーワード" + text + "を追加しました。";
        }
        return "振り分けキーワード " + text + " を追加しました。";
    }
    private static bool checkSelWordAddCommand(string msg)
    {
        if (msg.IndexOf("教育(") != 0)
        {
            return false;
        }
        if (msg.LastIndexOf(")") != msg.Length - 1)
        {
            return false;
        }
        int num = msg.IndexOf("+");
        if (num < 4 || num + 1 >= msg.Length - 1)
        {
            num = msg.IndexOf("*");
            if (num < 4 || num + 1 >= msg.Length - 1)
            {
                return false;
            }
        }
        return true;
    }
    private static string replaceReg(string msg, msgVoiceRoid option)
    {
        foreach (TRegReplace current in option.lstRegReplace)
        {
            RegexOptions regexOptions = RegexOptions.None;
            if (current.bIgnore)
            {
                regexOptions |= RegexOptions.IgnoreCase;
            }
            if (current.bSingleline)
            {
                regexOptions |= RegexOptions.Singleline;
            }
            if (current.bMultiline)
            {
                regexOptions |= RegexOptions.Multiline;
            }
            Regex regex = new Regex(current.sRead, regexOptions);
            msg = regex.Replace(msg, current.sReplace);
        }
        return msg;
    }
    private static string replaceSimple(string msg, msgVoiceRoid option)
    {
        foreach (TSimpleReplace current in option.lstSimpleReplace)
        {
            msg = VRMessage.StringReplace(msg, current.sRead, current.sReplace, -1, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.IgnoreWidth);
        }
        return msg;
    }
    public static string StringReplace(string input, string oldValue, string newValue, int count, CompareInfo compInfo, CompareOptions compOptions)
    {
        if (input == null || input.Length == 0 || oldValue == null || oldValue.Length == 0 || count == 0)
        {
            return input;
        }
        if (compInfo == null)
        {
            compInfo = CultureInfo.InvariantCulture.CompareInfo;
            compOptions = CompareOptions.Ordinal;
        }
        int length = input.Length;
        int length2 = oldValue.Length;
        StringBuilder stringBuilder = new StringBuilder(length);
        int num = 0;
        int num2 = 0;
        while (true)
        {
            int num3 = compInfo.IndexOf(input, oldValue, num, compOptions);
            if (num3 < 0)
            {
                break;
            }
            stringBuilder.Append(input.Substring(num, num3 - num));
            stringBuilder.Append(newValue);
            num = num3 + length2;
            num2++;
            if (num2 == count)
            {
                goto Block_7;
            }
            if (num >= length)
            {
                goto IL_B0;
            }
        }
        stringBuilder.Append(input.Substring(num));
        goto IL_B0;
        Block_7:
        stringBuilder.Append(input.Substring(num));
        IL_B0:
        return stringBuilder.ToString();
    }
    public static string StringReplace(string input, string oldValue, string newValue, int count, bool ignoreCase)
    {
        if (ignoreCase)
        {
            return VRMessage.StringReplace(input, oldValue, newValue, count, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.IgnoreWidth | CompareOptions.OrdinalIgnoreCase);
        }
        return VRMessage.StringReplace(input, oldValue, newValue, count, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.Ordinal);
    }
    public static string StringReplace(string input, string oldValue, string newValue, int count)
    {
        return VRMessage.StringReplace(input, oldValue, newValue, count, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.Ordinal);
    }
}
}