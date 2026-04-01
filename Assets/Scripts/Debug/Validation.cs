using System.Runtime.CompilerServices;
using UnityEngine;

public static class Validate
{
    static bool IsNull(object obj)
    {
        if (obj is Object unityObj)
            return unityObj == null;
        
        return obj == null;
    }

    static void LogNullWarning(string variableName, string callerName, string filePath, int lineNumber)
    {
        Debug.LogWarning($"[Null Check Failed] '{variableName}' is null in {callerName}()\nFile: {filePath} (Line {lineNumber})");
    }

    // 1 Variable
    public static bool AnyNull(
        object obj1,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        if (IsNull(obj1))
        {
            if (logWarnings) LogNullWarning(name1, caller, file, line);
            return true;
        }
        return false;
    }

    // 2 Variables
    public static bool AnyNull(
        object obj1, object obj2,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        return hasNull;
    }

    // 3 Variables
    public static bool AnyNull(
        object obj1, object obj2, object obj3,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerArgumentExpression("obj3")] string name3 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        if (IsNull(obj3)) { if (logWarnings) LogNullWarning(name3, caller, file, line); hasNull = true; }
        return hasNull;
    }

    #region Expanded Overloads (4 to 8 Variables)
    
    // 4 Variables
    public static bool AnyNull(
        object obj1, object obj2, object obj3, object obj4,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerArgumentExpression("obj3")] string name3 = "",
        [CallerArgumentExpression("obj4")] string name4 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        if (IsNull(obj3)) { if (logWarnings) LogNullWarning(name3, caller, file, line); hasNull = true; }
        if (IsNull(obj4)) { if (logWarnings) LogNullWarning(name4, caller, file, line); hasNull = true; }
        return hasNull;
    }

    // 5 Variables
    public static bool AnyNull(
        object obj1, object obj2, object obj3, object obj4, object obj5,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerArgumentExpression("obj3")] string name3 = "",
        [CallerArgumentExpression("obj4")] string name4 = "",
        [CallerArgumentExpression("obj5")] string name5 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        if (IsNull(obj3)) { if (logWarnings) LogNullWarning(name3, caller, file, line); hasNull = true; }
        if (IsNull(obj4)) { if (logWarnings) LogNullWarning(name4, caller, file, line); hasNull = true; }
        if (IsNull(obj5)) { if (logWarnings) LogNullWarning(name5, caller, file, line); hasNull = true; }
        return hasNull;
    }

    // 6 Variables
    public static bool AnyNull(
        object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerArgumentExpression("obj3")] string name3 = "",
        [CallerArgumentExpression("obj4")] string name4 = "",
        [CallerArgumentExpression("obj5")] string name5 = "",
        [CallerArgumentExpression("obj6")] string name6 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        if (IsNull(obj3)) { if (logWarnings) LogNullWarning(name3, caller, file, line); hasNull = true; }
        if (IsNull(obj4)) { if (logWarnings) LogNullWarning(name4, caller, file, line); hasNull = true; }
        if (IsNull(obj5)) { if (logWarnings) LogNullWarning(name5, caller, file, line); hasNull = true; }
        if (IsNull(obj6)) { if (logWarnings) LogNullWarning(name6, caller, file, line); hasNull = true; }
        return hasNull;
    }

    // 7 Variables
    public static bool AnyNull(
        object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerArgumentExpression("obj3")] string name3 = "",
        [CallerArgumentExpression("obj4")] string name4 = "",
        [CallerArgumentExpression("obj5")] string name5 = "",
        [CallerArgumentExpression("obj6")] string name6 = "",
        [CallerArgumentExpression("obj7")] string name7 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        if (IsNull(obj3)) { if (logWarnings) LogNullWarning(name3, caller, file, line); hasNull = true; }
        if (IsNull(obj4)) { if (logWarnings) LogNullWarning(name4, caller, file, line); hasNull = true; }
        if (IsNull(obj5)) { if (logWarnings) LogNullWarning(name5, caller, file, line); hasNull = true; }
        if (IsNull(obj6)) { if (logWarnings) LogNullWarning(name6, caller, file, line); hasNull = true; }
        if (IsNull(obj7)) { if (logWarnings) LogNullWarning(name7, caller, file, line); hasNull = true; }
        return hasNull;
    }

    // 8 Variables
    public static bool AnyNull(
        object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8,
        bool logWarnings = true,
        [CallerArgumentExpression("obj1")] string name1 = "",
        [CallerArgumentExpression("obj2")] string name2 = "",
        [CallerArgumentExpression("obj3")] string name3 = "",
        [CallerArgumentExpression("obj4")] string name4 = "",
        [CallerArgumentExpression("obj5")] string name5 = "",
        [CallerArgumentExpression("obj6")] string name6 = "",
        [CallerArgumentExpression("obj7")] string name7 = "",
        [CallerArgumentExpression("obj8")] string name8 = "",
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        bool hasNull = false;
        if (IsNull(obj1)) { if (logWarnings) LogNullWarning(name1, caller, file, line); hasNull = true; }
        if (IsNull(obj2)) { if (logWarnings) LogNullWarning(name2, caller, file, line); hasNull = true; }
        if (IsNull(obj3)) { if (logWarnings) LogNullWarning(name3, caller, file, line); hasNull = true; }
        if (IsNull(obj4)) { if (logWarnings) LogNullWarning(name4, caller, file, line); hasNull = true; }
        if (IsNull(obj5)) { if (logWarnings) LogNullWarning(name5, caller, file, line); hasNull = true; }
        if (IsNull(obj6)) { if (logWarnings) LogNullWarning(name6, caller, file, line); hasNull = true; }
        if (IsNull(obj7)) { if (logWarnings) LogNullWarning(name7, caller, file, line); hasNull = true; }
        if (IsNull(obj8)) { if (logWarnings) LogNullWarning(name8, caller, file, line); hasNull = true; }
        return hasNull;
    }

    #endregion
}

public static class ConditionalLoggers
{
    #region LogAnd (&&) Overloads

    // 2 Variables
    public static bool LogAnd(
        bool c1, bool c2,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "")
    {
        bool result = c1 && c2;
        Debug.Log($"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2}");
        return result;
    }

    // 3 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "")
    {
        bool result = c1 && c2 && c3;
        Debug.Log($"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3}");
        return result;
    }

    // 4 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "")
    {
        bool result = c1 && c2 && c3 && c4;
        Debug.Log($"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4}");
        return result;
    }

    // 5 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4, bool c5,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "")
    {
        bool result = c1 && c2 && c3 && c4 && c5;
        Debug.Log(
            $"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5}");
        return result;
    }

    // 6 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "")
    {
        bool result = c1 && c2 && c3 && c4 && c5 && c6;
        Debug.Log(
            $"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6}");
        return result;
    }

    // 7 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "")
    {
        bool result = c1 && c2 && c3 && c4 && c5 && c6 && c7;
        Debug.Log(
            $"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7}");
        return result;
    }

    // 8 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "",
        [CallerArgumentExpression("c8")] string e8 = "")
    {
        bool result = c1 && c2 && c3 && c4 && c5 && c6 && c7 && c8;
        Debug.Log(
            $"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7} | [{e8}]: {c8}");
        return result;
    }

    // 9 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8, bool c9,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "",
        [CallerArgumentExpression("c8")] string e8 = "",
        [CallerArgumentExpression("c9")] string e9 = "")
    {
        bool result = c1 && c2 && c3 && c4 && c5 && c6 && c7 && c8 && c9;
        Debug.Log(
            $"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7} | [{e8}]: {c8} | [{e9}]: {c9}");
        return result;
    }

    // 10 Variables
    public static bool LogAnd(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8, bool c9, bool c10,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "",
        [CallerArgumentExpression("c8")] string e8 = "",
        [CallerArgumentExpression("c9")] string e9 = "",
        [CallerArgumentExpression("c10")] string e10 = "")
    {
        bool result = c1 && c2 && c3 && c4 && c5 && c6 && c7 && c8 && c9 && c10;
        Debug.Log(
            $"[AND] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7} | [{e8}]: {c8} | [{e9}]: {c9} | [{e10}]: {c10}");
        return result;
    }

    #endregion

    #region LogOr (||) Overloads

    // 2 Variables
    public static bool LogOr(
        bool c1, bool c2,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "")
    {
        bool result = c1 || c2;
        Debug.Log($"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2}");
        return result;
    }

    // 3 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "")
    {
        bool result = c1 || c2 || c3;
        Debug.Log($"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3}");
        return result;
    }

    // 4 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "")
    {
        bool result = c1 || c2 || c3 || c4;
        Debug.Log($"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4}");
        return result;
    }

    // 5 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4, bool c5,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "")
    {
        bool result = c1 || c2 || c3 || c4 || c5;
        Debug.Log($"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5}");
        return result;
    }

    // 6 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "")
    {
        bool result = c1 || c2 || c3 || c4 || c5 || c6;
        Debug.Log(
            $"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6}");
        return result;
    }

    // 7 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "")
    {
        bool result = c1 || c2 || c3 || c4 || c5 || c6 || c7;
        Debug.Log(
            $"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7}");
        return result;
    }

    // 8 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "",
        [CallerArgumentExpression("c8")] string e8 = "")
    {
        bool result = c1 || c2 || c3 || c4 || c5 || c6 || c7 || c8;
        Debug.Log(
            $"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7} | [{e8}]: {c8}");
        return result;
    }

    // 9 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8, bool c9,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "",
        [CallerArgumentExpression("c8")] string e8 = "",
        [CallerArgumentExpression("c9")] string e9 = "")
    {
        bool result = c1 || c2 || c3 || c4 || c5 || c6 || c7 || c8 || c9;
        Debug.Log(
            $"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7} | [{e8}]: {c8} | [{e9}]: {c9}");
        return result;
    }

    // 10 Variables
    public static bool LogOr(
        bool c1, bool c2, bool c3, bool c4, bool c5, bool c6, bool c7, bool c8, bool c9, bool c10,
        [CallerArgumentExpression("c1")] string e1 = "",
        [CallerArgumentExpression("c2")] string e2 = "",
        [CallerArgumentExpression("c3")] string e3 = "",
        [CallerArgumentExpression("c4")] string e4 = "",
        [CallerArgumentExpression("c5")] string e5 = "",
        [CallerArgumentExpression("c6")] string e6 = "",
        [CallerArgumentExpression("c7")] string e7 = "",
        [CallerArgumentExpression("c8")] string e8 = "",
        [CallerArgumentExpression("c9")] string e9 = "",
        [CallerArgumentExpression("c10")] string e10 = "")
    {
        bool result = c1 || c2 || c3 || c4 || c5 || c6 || c7 || c8 || c9 || c10;
        Debug.Log(
            $"[OR] Result: {result} -> [{e1}]: {c1} | [{e2}]: {c2} | [{e3}]: {c3} | [{e4}]: {c4} | [{e5}]: {c5} | [{e6}]: {c6} | [{e7}]: {c7} | [{e8}]: {c8} | [{e9}]: {c9} | [{e10}]: {c10}");
        return result;
    }
}

#endregion