//This file is responsible for parsing the C# source code and providing the syntax highlighting.
namespace Compiler;

public class CSharpParser{
    private int count;
    private string[] lines;
    protected SyntaxTree? tree;
    public CSharpParser(string source){   //Saves the code in the array
        lines = File.ReadLines(source).ToArray<string>(); //of strings.
        tree = new SyntaxTree();
    }
    /// <summary>
    /// The main method of the class. It executes the parsing process for every
    /// line until it exhausts the source array. Returns true if succeeded.
    /// </summary>
    public bool Parse(){
        try {
            for (count = 0; count < lines.Length; ++count){
                lines[count] = this.clearComments();
                if (lines[count] == "") continue;
                tree = this.divideIntoLexemes();
                tree = this.parseLexemes();
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
        //Step 1. Remove the rest of the lines[count] if multiline comment was not closed.
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
        if (start != -1){
            int end = lines[count].IndexOf("*/");
            if (end != -1){
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
    /// parentheses and identifiers. It returns the syntax tree with the lexemes.
    /// </summary>
    protected SyntaxTree divideIntoLexemes(){
        List<string> lexemes = lines[count].Split('"').ToList<string>();
        foreach (string lexeme in lexemes){
            if (lexeme.Contains('"')){
                tree.AddLast(new Lexeme(token.String, lexeme));
            }

        }
        return tree;
    }
    /// <summary>
    /// This method matches the lexemes' type and produces the new syntax tree
    /// that contains its colour to be displayed. It also adds ids to the lookup table.
    /// </summary>
    protected SyntaxTree parseLexemes(){
        return tree;
    }
}