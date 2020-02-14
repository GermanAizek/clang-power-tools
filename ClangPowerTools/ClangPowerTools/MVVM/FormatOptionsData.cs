﻿using ClangPowerTools.MVVM.Interfaces;
using ClangPowerTools.MVVM.Models;
using System.Collections.Generic;

namespace ClangPowerTools
{
  public class FormatOptionsData
  {
    public static List<IFormatOption> FormatOptions = new List<IFormatOption>()
    {
      new FormatOptionModel{ Name = "AccessModifierOffset", Paramater = "int", Description = "The extra indent or outdent of access modifiers, e.g. public:.", Input = "0"},
      new FormatOptionToggleModel{Name = "AlignEscapedNewlinesLeft", Paramater = "bool", Description = "If \"true\", aligns escaped newlines as far left as possible.\r\n\r\nOtherwise puts them into the right-most column. This does not necessarily mean flushing lings to the left but, rather, attempting to align the current line's left margin with the previous line's left margin", IsEnabled=false},
      new FormatOptionToggleModel{Name = "AlignTrailingComments", Paramater = "bool", Description = "If \"true\", aligns trailing comments", IsEnabled=false},
      new FormatOptionToggleModel{Name = "AllowAllParametersOfDeclarationOnNextLine", Paramater = "bool", Description = "If the function declaration doesn’t fit on a line, allow putting all parameters of a function declaration onto the next line even if BinPackParameters is false.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "AllowShortIfStatementsOnASingleLine", Paramater = "bool", Description = "If true, if (a) return; can be put on a single line.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "AllowShortLoopsOnASingleLine", Paramater = "bool", Description = "If true, while (true) continue; can be put on a single line.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "AlwaysBreakBeforeMultilineStrings", Paramater = "bool", Description = "If true, always break before multiline string literals.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "AlwaysBreakTemplateDeclarations", Paramater = "bool", Description = "If true, always break after the template<…> of a template declaration", IsEnabled=false},
      new FormatOptionToggleModel{Name = "BinPackParameters", Paramater = "bool", Description = "If false, a function call’s or function definition’s parameters will either all be on the same line or will have one line each.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "BreakBeforeBinaryOperators", Paramater = "bool", Description = "If true, binary operators will be placed after line breaks.", IsEnabled=false},
      new FormatOptionModel{ Name = "BreakBeforeBraces ", Paramater = "BraceBreakingStyle", Description = "- BS_Attach (in configuration: Attach) Always attach braces to surrounding context.\r\n- BS_Linux (in configuration: Linux) Like Attach, but break before braces on function, namespace and class definitions.\r\n- BS_Stroustrup (in configuration: Stroustrup) Like Attach, but break before function definitions.\r\n- BS_Allman (in configuration: Allman) Always break before braces.", Input = "Allman"},
      new FormatOptionToggleModel{Name = "BreakBeforeTernaryOperators", Paramater = "bool", Description = "If true, ternary operators will be placed after line breaks.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "BreakConstructorInitializersBeforeComma", Paramater = "bool", Description = "Always break constructor initializers before commas and align the commas with the colon.", IsEnabled=false},
      new FormatOptionModel{ Name = "ColumnLimit", Paramater = "unsigned", Description = "The column limit.\r\nA column limit of 0 means that there is no column limit. In this case, clang-format will respect the input’s line breaking decisions within statements unless they contradict other rules", Input = "100"},
      new FormatOptionModel{ Name = "CommentPragmas", Paramater = "std::string", Description = "A regular expression that describes comments with special meaning, which should not be split into lines or otherwise changed.", Input = string.Empty},
      new FormatOptionToggleModel{Name = "ConstructorInitializerAllOnOneLineOrOnePerLine", Paramater = "bool", Description = "If the constructor initializers don’t fit on a line, put each initializer on its own line", IsEnabled=false},
      new FormatOptionModel{ Name = "ConstructorInitializerIndentWidth ", Paramater = "unsigned", Description = "The number of characters to use for indentation of constructor initializer lists.", Input = "2"},
      new FormatOptionModel{ Name = "ContinuationIndentWidth ", Paramater = "unsigned", Description = "Indent width for line continuations", Input = "2"},
      new FormatOptionToggleModel{Name = "Cpp11BracedListStyle", Paramater = "bool", Description = "If true, format braced lists as best suited for C++11 braced lists.\r\nImportant differences: - No spaces inside the braced list. - No line break before the closing brace. - Indentation with the continuation indent, not with the block indent\r\nFundamentally, C++11 braced lists are formatted exactly like function calls would be formatted in their place. If the braced list follows a name (e.g. a type or variable name), clang-format formats as if the {} were the parentheses of a function call with that name. If there is no name, a zero-length name is assumed.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "DeriveLineEnding", Paramater = "bool", Description = "Analyze the formatted file for the most used line ending (\r\n or \n). UseCRLF is only used as a fallback if none can be derived.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "DerivePointerAlignment", Paramater = "bool", Description = "If true, analyze the formatted file for the most common alignment of & and *. Pointer and reference alignment styles are going to be updated according to the preferences found in the file. PointerAlignment is then used only as fallback", IsEnabled=false},
      new FormatOptionToggleModel{Name = "IndentCaseLabels", Paramater = "bool", Description = "Indent case labels one level from the switch statement.\r\nWhen false, use the same indentation level as for the switch statement. Switch statement body is always indented one level more than case labels.", IsEnabled=false},
      new FormatOptionModel{ Name = "IndentWidth ", Paramater = "unsigned", Description = "The number of columns to use for indentation.", Input = "4"},
      new FormatOptionModel{ Name = "MaxEmptyLinesToKeep ", Paramater = "unsigned", Description = "The maximum number of consecutive empty lines to keep.", Input = "2"},
      new FormatOptionModel{ Name = "NamespaceIndentation ", Paramater = "NamespaceIndentationKind", Description = "The indentation used for namespaces.\r\nPossible values:\r\n- NI_None (in configuration: None) Don’t indent in namespaces.\r\n- NI_Inner (in configuration: Inner) Indent only in inner namespaces (nested in other namespaces).\r\n- NI_All (in configuration: All) Indent in all namespaces.", Input = "None"},
      new FormatOptionModel{ Name = "PenaltyBreakBeforeFirstCallParameter ", Paramater = "unsigned", Description = "The penalty for breaking a function call after \"call(\".", Input = "100"},
      new FormatOptionModel{ Name = "PenaltyBreakComment", Paramater = "unsigned", Description = "The penalty for each line break introduced inside a comment.", Input = "100"},
      new FormatOptionModel{ Name = "PenaltyBreakFirstLessLess", Paramater = "unsigned", Description = "The penalty for breaking before the first <<.", Input = "0"},
      new FormatOptionModel{ Name = "PenaltyBreakString", Paramater = "unsigned", Description = "The penalty for each line break introduced inside a string literal.", Input = "100"},
      new FormatOptionModel{ Name = "PenaltyExcessCharacter", Paramater = "unsigned", Description = "The penalty for each character outside of the column limit.", Input = "1"},
      new FormatOptionModel{ Name = "PenaltyReturnTypeOnItsOwnLine", Paramater = "unsigned", Description = "Penalty for putting the return type of a function onto its own line.", Input = "20"},
      new FormatOptionToggleModel{Name = "SpaceBeforeAssignmentOperators", Paramater = "bool", Description = "If false, spaces will be removed before assignment operators.", IsEnabled=false},
      new FormatOptionModel{ Name = "SpaceBeforeParens", Paramater = "SpaceBeforeParensOptions", Description = "Defines in which cases to put a space before opening parentheses.\r\nPossible values:\r\n- SBPO_Never (in configuration: Never) Never put a space before opening parentheses.\r\n- SBPO_ControlStatements (in configuration: ControlStatements) Put a space before opening parentheses only after control statement keywords (for/if/while...).\r\n- SBPO_Always (in configuration: Always) Always put a space before opening parentheses, except when it’s prohibited by the syntax rules (in function-like macro definitions) or when determined by other style rules (after unary operators, opening parentheses, etc.)", Input = "Always"},
      new FormatOptionToggleModel{Name = "AlignTrailingComments", Paramater = "bool", Description = "If \"true\", spaces may be inserted into \"()\".", IsEnabled=false},
      new FormatOptionModel{ Name = "SpacesBeforeTrailingComments", Paramater = "unsigned", Description = "The number of spaces to before trailing line comments.", Input = "1"},
      new FormatOptionToggleModel{Name = "SpacesInAngles", Paramater = "bool", Description = "If true, spaces will be inserted after \"<\" and before \">\" in template argument lists", IsEnabled=false},
      new FormatOptionToggleModel{Name = "SpacesInCStyleCastParentheses", Paramater = "bool", Description = "If true, spaces may be inserted into C style casts.", IsEnabled=false},
      new FormatOptionToggleModel{Name = "SpacesInContainerLiterals", Paramater = "bool", Description = "If true, spaces are inserted inside container literals (e.g. ObjC and JavaScript array and dict literals).", IsEnabled=false},
      new FormatOptionToggleModel{Name = "SpacesInParentheses", Paramater = "bool", Description = "If true, spaces will be inserted after ‘(‘ and before ‘)’.", IsEnabled=false},
      new FormatOptionModel{ Name = "Standard", Paramater = "LanguageStandard", Description = "Parse and format C++ constructs compatible with this standard.\r\nPossible values:\r\n- LS_Cpp03 (in configuration: c++03) Parse and format as C++03. Cpp03 is a deprecated alias for c++03\r\n- LS_Cpp11 (in configuration: c++11) Parse and format as C++11.\r\n- LS_Cpp14 (in configuration: c++14) Parse and format as C++14.\r\n- LS_Cpp17 (in configuration: c++17) Parse and format as C++17.\r\n- LS_Cpp20 (in configuration: c++20) Parse and format as C++20.\r\n- LS_Latest (in configuration: Latest) Parse and format using the latest supported language version. Cpp11 is a deprecated alias for Latest", Input = "Cpp20"},
      new FormatOptionModel{ Name = "TabWidth", Paramater = "unsigned", Description = "The number of columns used for tab stops.", Input = "4"},
      new FormatOptionModel{ Name = "UseTab", Paramater = "UseTabStyle", Description = "The way to use tab characters in the resulting file.\r\nPossible values:\r\n- UT_Never (in configuration: Never) Never use tab.\r\n- UT_ForIndentation (in configuration: ForIndentation) Use tabs only for indentation.\r\n- UT_ForContinuationAndIndentation (in configuration: ForContinuationAndIndentation) Use tabs only for line continuation and indentation.\r\n- UT_Always (in configuration: Always) Use tabs whenever we need to fill whitespace that spans at least from one tab stop to the next on", Input = "Never"}
    };
  }
}
