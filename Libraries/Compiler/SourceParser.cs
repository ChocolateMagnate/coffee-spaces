//This file is responsible for parsing the C# source code and providing the syntax highlighting.
namespace Compiler;
/// <summary>
/// Encapsulates language-specific information in a separate object. Contains
/// the definitions of the keywords, operators, separators and verity of tokens.
/// </summary>
public class Language{
    public token[] Tokens;
    public string[] Keywords;
    public string[] Operators;
    public string[] Separators;
    public Language(string[] keywords, string[] operators, string[] separators, token[] tokens) {
        this.Tokens = tokens;
        this.Keywords = keywords;
        this.Operators = operators;
        this.Separators = separators;
    }
}

public class SourceParser{
    private int count;
    private Lexeme[] tree;
    private Language language;
    private string[] lines, lexemes;
    private Dictionary<token, string> identifiers;
    public SourceParser(string source, Language lang) {   //Saves the code in an array of lines.
        lines = File.ReadAllText(source).Split(Environment.NewLine).ToArray<string>();
        tree = new Lexeme[lines.Length];  
        language = lang;
    }
    /// <summary>
    /// The main method of the class. It executes the parsing process for every
    /// line until it exhausts the source array. Returns true if succeeded.
    /// </summary>
    public bool Parse() {
        try {
            for (count = 0; count < lines.Length; count++) {
                lines[count] = this.clearComments();
                if (lines[count] == "") continue;
                    //tree = this.divideIntoLexemes();
                //tree = this.parseLexemes();
            }
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return false;
        }
        return true;
    }
    /// <summary>
    /// This method removes the comments from the line. It handles all 
    /// edge cases, including: regular comments with //, multiline comments
    /// with /* */, and anticipates multiple comments in one line. It also
    /// makes sure the string will be cleared if multiline comment was not closed
    /// until it finds the closing tag thanks for the recursive call.
    /// </summary>
    protected string clearComments(bool closedComment = true) {
        //To add non-C like comments, such as Pythonic.
        //Step 1. Remove the rest of the lines if multiline comment was not closed.
        if (!closedComment) {
            int end = lines[count].IndexOf("*/");
            if (end != -1) {
                lines[count] = lines[count].Substring(end + 2);
                closedComment = true;
            }
        }
        //Step 2. Remove the regular comments.
        lines[count] = lines[count].Split("//")[0];
        //Step 3. Remove the multiline comments.
        int start = lines[count].IndexOf("/*");
        if (start != -1 && start != 0) {
            int end = lines[count].IndexOf("*/");
            if (end != -1 && end != 0) {
                lines[count] = lines[count].Substring(0, start) + lines[count].Substring(end + 2);
            }else{
                lines[count] = lines[count].Substring(0, start);
                closedComment = false;
            }
        }
        return lines[count];
    }
    /// <summary>
    /// This method divides the line into meaningful parts that are ready
    /// to be parsed as individual tokens in this order of priority: strings,
    /// parentheses and identifiers. It returns the array of the lexemes.
    /// </summary>
    protected void divideIntoLexemes() {
        lexemes = lines[count].Split(language.Separators, StringSplitOptions.RemoveEmptyEntries);
    }
    /// <summary>
    /// This method matches the lexemes' type and produces the new syntax tree
    /// that contains its colour to be displayed. It also adds ids to the lookup table.
    /// </summary>
    protected void parseLexemes() {
        int step = 0; token flag = token.Unknown; //The flag is used to determine 
        foreach (var lexeme in lexemes) { //the type of the id that follows the keyword.
            if (language.Keywords.Contains(lexeme)) {
                tree[step] = new Lexeme(lexeme, token.Keyword);
                switch (lexeme) {
                    case "class":
                        flag = token.Class;     break;
                    case "interface":
                        flag = token.Interface; break;
                    case "enum":
                        flag = token.Enum;      break;
                    case "struct":
                        flag = token.Struct;    break;
                    case "namespace":
                        flag = token.Namespace; break;
                    case "public":
                    case "private":
                    case "protected":
                        flag = token.AccessModifier; break;
                }
                ++step; continue;
            } else if (language.Operators.Contains(lexeme)) {
                tree[step] = new Lexeme(lexeme, token.Operator);
                ++step; continue;
            } else if (lexeme.StartsWith("\"") && lexeme.EndsWith("\"")) {
                tree[step] = new Lexeme(lexeme, token.String);
                ++step; continue;
            } else if (double.TryParse(lexeme, out double number)) {
                tree[step] = new Lexeme(lexeme, token.Number);
                ++step; continue;
            }
            //Anticipating special cases.
            switch (lexeme) {
                case "true":
                case "false":
                    tree[step] = new Lexeme(lexeme, token.Boolean);
                    ++step; break;
                case "null": 
                    tree[step] = new Lexeme(lexeme, token.Null);
                    ++step; break;
                case "(":
                    tree[step] = new Lexeme(lexeme, token.LeftParenthesis);
                    ++step; break;
                case ")":
                    tree[step] = new Lexeme(lexeme, token.RightParenthesis);
                    ++step; break;
                case "{":
                    tree[step] = new Lexeme(lexeme, token.LeftBrace);
                    ++step; break;
                case "}":
                    tree[step] = new Lexeme(lexeme, token.RightBrace);
                    ++step; break;
                case "[":
                    tree[step] = new Lexeme(lexeme, token.LeftBracket);
                    ++step; break;
                case "]":
                    tree[step] = new Lexeme(lexeme, token.RightBracket);
                    ++step; break;
                case "<":
                    tree[step] = new Lexeme(lexeme, token.LeftAngleBracket);
                    ++step; break;
                case ">":
                    tree[step] = new Lexeme(lexeme, token.RightAngleBracket);
                    ++step; break;
                case ":":
                    tree[step] = new Lexeme("Colon", token.Separator);
                    ++step; break;
                case ";":
                    tree[step] = new Lexeme("Semicolon", token.Separator);
                    ++step; break;
                case ",":
                    tree[step] = new Lexeme("Comma", token.Separator);
                    ++step; break;
                case ".":
                    tree[step] = new Lexeme("Dot", token.Separator);
                    ++step; break;
                case "#":
                    tree[step] = new Lexeme("Hash", token.Separator);
                    ++step; break;
                case "@":
                    tree[step] = new Lexeme("At", token.Separator);
                    ++step; break;
                case "$":
                    tree[step] = new Lexeme("DollarSign", token.Separator);
                    ++step; break;
                default: //If the lexeme is not a keyword, operator,  
                        //separator, number or string, it is an identifier.
                   identifiers[flag] = lexeme;
                   tree[step] = new Lexeme(lexeme, flag);
                    ++step; break;
            }
        }
    }
}
        
/*string[] separators = {" ", "(", ")", "{", "}", "[", "]", ";", ",", ".", ":",
    "+", "-", "*", "/", "%", "=", "!", "<", ">", "&", "|",  "^", "~", "?", "++",
    "--", "+=", "-=", "*=", "/=", "%=", "==", "===", "!=", "!==", "<=", ">=", "&&",
    "||", "&=", "|=", "^=", "<<", ">>", "<<=", ">>=", "??", "=>", "::", "/*", "//"};
*/