//This file is responsible for the general function 
//set of compilers dealing with tokens, syntax tree.
namespace Compiler;
/// <summary>
/// These are the aliases for the types of tokens.
/// </summary>
enum Tokens{
    Operator, Identifier, Keyword, String, Number, Whitespace, Newline, Unknown
}
/// <summary>
/// Instances of this class are used to store the information about a lexeme.
/// </summary>
class Lexeme{
    public Tokens Type;
    public string Content;
    public Lexeme(Tokens type, string content){
        this.Type = type;
        this.Content = content;
    }
}
/// <summary>
/// The Abstract Syntax Tree that represents the source code as a linked tree of lexemes.
/// </summary>
class SyntaxTree: LinkedList<Lexeme>, IEnumerable<Lexeme>{
    protected LinkedList<Lexeme> Lexemes;
    public SyntaxTree(){
        this.Lexemes = new LinkedList<Lexeme>();
    }
}
