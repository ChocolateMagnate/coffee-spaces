/*This file is responsible for the basic code analysis on the example
 * of C#. You can find here enterprise-ready solution for tokenisation 
 * into lexemes, comment clearing and fetching raw text into colours. */
using Json;
using System.Drawing;
namespace Compiler;
/// <summary>
/// These are the aliases for the sorts tokens may represent. It mixes
/// the general types as operators or keywords with the specific types
/// that only identifiers may possess, including classes, functions, preprocessors.
/// </summary>
public enum token{
    Keyword, Operator, Separator, String, Number, Boolean, Null, Unknown, Variable, Constant, 
    Function, Class, Namespace, Enum, Interface, Struct, Property, Field, Method, AccessModifier,
    Constructor, Destructor, Parameter, Preprocessor,  LeftAngleBracket, RightAngleBracket, 
    LeftBracket, RightBracket, LeftBrace, RightBrace, LeftParenthesis, RightParenthesis, 
}
/// <summary>
/// Instances of this class are used to store the information about a lexeme.
/// </summary>

public struct Colours {
    Color Keyword = Color.IndianRed;
    Color Operator = Color.White;
    Color String = Color.Beige;
    Color Comment = Color.Gray;
    Color Number = Color.Orange;
    Color Constant = Color.Blue;
    public Colours(){}
}
public class Lexeme{
    public token Type;
    public string Content;
    public Lexeme(string content, token type){
        this.Type = type;
        this.Content = content;
    }
}
/// <summary>
/// Encapsulates language-specific information in a separate object. Contains
/// the definitions of the keywords, operators, separators and verity of tokens.
/// </summary>
public struct Language{
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

public class SourceParser {
    private int count;
    public bool Successful;
    private Lexeme[] tree;
    private Language language;
    private string[] lines, lexemes;
    private Dictionary<token, string> identifiers;
    public SourceParser(string source, Language language) { //Saves the code in an array of lines.
        this.lines = File.ReadAllText(source).Split(Environment.NewLine);
        this.identifiers = new Dictionary<token, string>();
        this.lexemes = new string[lines.Length];
        this.tree = new Lexeme[lines.Length];  
        this.language = language;

    }
    /// <summary>
    /// The main method of the class. It executes the parsing process for every 
    /// line until it exhausts the source array. Assigns true to the property 
    /// Successful if succeeded to verify type safety.
    /// </summary>
    public void Parse() {
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
            this.Successful = false;
        }
        this.Successful = true;
    }
    /// <summary>
    /// Provides the array of the lexemes from the source code and their token types.
    /// </summary>
    public Lexeme[] GetLexemes() {
        return tree;
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
        lexemes = lines[count].Split(language.Separators.Concat(language.Operators)
            .ToArray<string>(), StringSplitOptions.RemoveEmptyEntries);

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
internal class Program {
    private static Language CSharp = new Language( //Keywords
        new string[] {"abstract", "as", "base", "bool", "break", "byte", "case", "catch",
            "char", "class", "const", "continue", "default", "delegate", "do","", "double",
            "else", "enum", "explicit", "implicit", "false", "float", "for", "foreach",
            "while", "if", "in", "int", "interface", "internal",  "long","namespace",
            "new", "out", "override", "params", "private","public", "protected", "readonly",
            "ref", "return", "sbyte", "sealed", "short", "static","void", "virtual"},
        //Operators and separators
        new string[] {"+", "-", "*", "/", "%", "=", "!", "<", ">", "&", "|",  "^", "~", "?",
            "++", "--", "+=", "-=", "*=", "/=", "%=", "==", "!=", "<=", ">=",""},
        new string[] {"(", ")", "{", "}", "[", "]", ";", ",", ".", ":"}, //Tokens
        new token[] {token.Keyword, token.Operator, token.Separator, token.String, token.Number,
            token.Boolean, token.Null, token.Unknown, token.Variable, token.Constant, token.Function,
            token.Class, token.Namespace, token.Enum, token.Interface, token.Struct, token.Property,
            token.AccessModifier, token.Field, token.Method, token.Parameter,token.LeftParenthesis,
            token.RightParenthesis, token.LeftBrace, token.RightBrace, token.LeftBracket,
            token.RightBracket, token.LeftAngleBracket, token.RightAngleBracket,
        });
    static void Main() {
        Lexeme[] items = new Lexeme[100];
        string path = "/workspaces/coffee-spaces/Libraries/Compiler/Code snippets/CSharpSample.cs";
        var code = new SourceParser(path, CSharp);
        code.Parse();
        if (code.Successful) items = code.GetLexemes();
        Console.WriteLine("Operation successful.");
        foreach (var item in items) Console.WriteLine(JsonParser.Serialize(item));
        Console.ReadKey();
    }
}