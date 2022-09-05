//This file is responsible for the general function 
//set of compilers dealing with tokens, syntax tree.
namespace Compiler;
/// <summary>
/// These are the aliases for the sorts tokens may represent. It mixes
/// the general types as operators or keywords with the specific types
/// that only identifiers may possess, including classes, functions, preprocessors.
/// </summary>
public enum token{
    Operator, Identifier, Keyword, String, Number, Whitespace, Newline, Unknown,
    LocalVariable, GlobalVariable, Function, Class, Namespace, Enum,
    Interface, Struct, Property, Field, Event, Method, Constructor,
    Destructor, Parameter, Preprocessor
}
/// <summary>
/// Instances of this class are used to store the information about a lexeme.
/// </summary>
public class Lexeme{
    public token Type;
    public string Content;
    public Lexeme(token type, string content){
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
/// <summary>
/// This table will serve to record all identifiers along with 
/// their types to evaluate their displayable colour in the editor.
/// </summary>
public class IdentifierTable: List<Lexeme>{
    protected string[] Keywords;
    protected string[] Operators;
    protected string[] Types;
    public List<Lexeme> Identifiers;
    public Dictionary<token, string> Shelves;
    public IdentifierTable(string[] keywords, string[] operators){
        this.Keywords = keywords;
        this.Operators = operators;
        this.Identifiers = new List<Lexeme>();
        this.Shelves = new Dictionary<token, string>();
    }
    public token Match(string identifier){
        if (this.Keywords.Contains(identifier)) return token.Keyword;
        if (this.Operators.Contains(identifier)) return token.Operator;
        foreach (var shelf in this.Shelves) if (shelf.Value.Contains(identifier)) return shelf.Key;
        return token.Unknown;
    }
    public Lexeme this[string key, string? filter]{
        get {}
        set {}
    }
}