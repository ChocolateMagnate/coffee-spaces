//This file is responsible for parsing the C# source code and providing the syntax highlighting.
namespace Compiler;
public class CSharpParser{
    private int count;
    private string[] lines;
    private SyntaxTree? tree;
    public CSharpParser(string source){   //Saves the code in the array
        lines = File.ReadLines(source).ToArray<string>(); //of strings.
    }
    /// <summary>
    /// The main method of the class. It executes the parsing process for every
    /// line until it exhausts the source array. Returns true if succeeded.
    /// </summary>
    public bool Parse(){
        for (count = 0; count < lines.Length; ++count){
            this.clearComments();

        }
        return true;
    }
    public string clearComments(bool closedComment = true){
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
}