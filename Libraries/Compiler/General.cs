//This file is responsible for the general function 
//set of compilers dealing with tokens, syntax tree.
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
public class Lexeme{
    public token Type;
    public string Content;
    public Lexeme(string content, token type){
        this.Type = type;
        this.Content = content;
    }
}
/// <summary>
/// The Abstract Syntax Tree that represents the source code as a linked tree of lexemes.
/// </summary>
public class SyntaxTree: LinkedList<Lexeme>, IEnumerable<Lexeme>{
    protected LinkedList<Lexeme> Lexemes;
    public SyntaxTree(){
        this.Lexemes = new LinkedList<Lexeme>();
    }
}
