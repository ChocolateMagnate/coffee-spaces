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
    public Language(string[] keywords, string[] operators, string[] separators, token[] tokens){
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
    private IdentifierTable identifiers;
    public SourceParser(string source, Language lang){   //Saves the code in an array of lines.
        lines = File.ReadAllText(source).Split(Environment.NewLine).ToArray<string>();
        tree = new Lexeme[lines.Length];  
        language = lang;
    }
    /// <summary>
    /// The main method of the class. It executes the parsing process for every
    /// line until it exhausts the source array. Returns true if succeeded.
    /// </summary>
    public bool Parse(){
        try {
            for (count = 0; count < lines.Length; count++) {
                lines[count] = this.clearComments();
                if (lines[count] == "") continue;
                    //tree = this.divideIntoLexemes();
                //tree = this.parseLexemes();
            }
        }
        catch (Exception e){
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
    protected string clearComments(bool closedComment = true){
        //To add non-C like comments, such as Pythonic.
        //Step 1. Remove the rest of the lines if multiline comment was not closed.
        if (!closedComment){
            int end = lines[count].IndexOf("*/");
            if (end != -1){
                lines[count] = lines[count].Substring(end + 2);
                closedComment = true;
            }
        }
        //Step 2. Remove the regular comments.
        lines[count] = lines[count].Split("//")[0];
        //Step 3. Remove the multiline comments.
        int start = lines[count].IndexOf("/*");
        if (start != -1 && start != 0){
            int end = lines[count].IndexOf("*/");
            if (end != -1 && end != 0){
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
    protected void divideIntoLexemes(){
        lexemes = lines[count].Split(language.Separators, StringSplitOptions.RemoveEmptyEntries);
    }
    /// <summary>
    /// This method matches the lexemes' type and produces the new syntax tree
    /// that contains its colour to be displayed. It also adds ids to the lookup table.
    /// </summary>
    protected void parseLexemes(){
        int step = 0;
        foreach (var lexeme in lexemes){
            if (language.Keywords.Contains(lexeme) {
                tree[step] = new Lexeme(token.Keyword, lexeme);
                ++step; continue;
            } else if (language.Operators.Contains(lexeme)){
                tree[step] = new Lexeme(token.Operator, lexeme);
                ++step; continue;
            } else if (double.TryParse(lexeme, out double number)) {
                tree[step] = new Lexeme(token.Number, lexeme);
                ++step; continue;
            } else if (lexeme.StartsWith("\"") && lexeme.EndsWith("\"")){
                tree[step] = new Lexeme(token.String, lexeme);
                ++step; continue;
            }
            //If the lexeme is not a keyword, operator, number or string, it is an identifier.
            switch (lexeme){
                case "true":
                    tree[step] = new Lexeme(token.Boolean, lexeme);
                    ++step; break;
                case "false":
                    tree[step] = new Lexeme(token.Boolean, lexeme);
                    ++step; break;
                case "null": 
                    tree[step] = new Lexeme(token.Null, lexeme);
                    ++step; break;
                case "(":
                    tree[step] = new Lexeme(token.LeftParenthesis, lexeme);
                    ++step; break;
                case ")":
                    tree[step] = new Lexeme(token.RightParenthesis, lexeme);
                    ++step; break;
                case "{":
                    tree[step] = new Lexeme(token.LeftBrace, lexeme);
                    ++step; break;
                case "}":
                    tree[step] = new Lexeme(token.RightBrace, lexeme);
                    ++step; break;
                case "[":
                    tree[step] = new Lexeme(token.LeftBracket, lexeme);
                    ++step; break;
                case "]":
                    tree[step] = new Lexeme(token.RightBracket, lexeme);
                    ++step; break;
                case "<":
                    tree[step] = new Lexeme(token.LeftAngleBracket, lexeme);
                    ++step; break;
                case ">":
                    tree[step] = new Lexeme(token.RightAngleBracket, lexeme);
                    ++step; break;
                case ":":
                    tree[step] = new Lexeme(token.Separator, "Colon");
                    ++step; break;
                case ";":
                    tree[step] = new Lexeme(token.Separator, "Semicolon");
                    ++step; break;
                case ",":
                    tree[step] = new Lexeme(token.Separator, "Comma");
                    ++step; break;
                case ".":
                    tree[step] = new Lexeme(token.Separator, "Dot");
                    ++step; break;
                case "#":
                    tree[step] = new Lexeme(token.Separator, "Hash");
                    ++step; break;
                case "@":
                    tree[step] = new Lexeme(token.Separator, "At");
                    ++step; break;
                case "$":
                    tree[step] = new Lexeme(token.Separator, "Dollar");
                    ++step; break;
                case "^":
                    tree[step] = new Lexeme(token.Separator, "Caret");
                    ++step; break;
                default:

                    tree[step] = new Lexeme(token.Identifier, lexeme);
                    ++step; break;
            }
    }
}
        
/*string[] separators = {" ", "(", ")", "{", "}", "[", "]", ";", ",", ".", ":",
    "+", "-", "*", "/", "%", "=", "!", "<", ">", "&", "|",  "^", "~", "?", "++",
    "--", "+=", "-=", "*=", "/=", "%=", "==", "===", "!=", "!==", "<=", ">=", "&&",
    "||", "&=", "|=", "^=", "<<", ">>", "<<=", ">>=", "??", "=>", "::", "/*", "//"};
*/