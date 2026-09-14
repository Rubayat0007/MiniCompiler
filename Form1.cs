using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static compiler2026.CompilerProject;


namespace compiler2026
{
    public partial class CompilerProject : Form
    {
        public string sourceProgram;
        public int currentPointer;
        public token currentToken;
        public IdentifierTable Identifiers;
        public TemporaryVariableTable tempVars;
        public QuadrupleTable midCodes;

        private void print(string s)
        {
            listBox.Items.Add(s);
        }
        private void printToken(token t)
        {
            listBox.Items.Add($"[{t.type}, {t.value}]");
        }
        private void buttonTokenize_Click(object sender, EventArgs e)
        {
            sourceProgram = textBox1.Text + "#";
            listBox.Items.Clear();
            listBox1.Items.Clear();
            print(sourceProgram);
            currentPointer = 0;// Get the first token
            currentToken = tokenizer();// Print the first token using printToken method
            printToken(currentToken);
            // Loop to process all tokens until the end-of-input marker
            while (currentToken.type != "#")
            {
                currentToken = tokenizer();  // Get the next token
                printToken(currentToken);  // Print each token within square brackets
            }
            listBox.Items.Add("end..");// Indicate the end of tokenization
        }


        public CompilerProject()
        {
            InitializeComponent();
            this.KeyPreview = true; // Enable key preview
            listBox.KeyDown += new KeyEventHandler(listBox_KeyDown); // Attach KeyDown event handler for listBox
            listBox1.KeyDown += new KeyEventHandler(listBox1_KeyDown); // Attach KeyDown event handler for listBox1
        }
        private void CopyAllItemsToClipboard(ListBox listBox)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in listBox.Items)
            {
                sb.AppendLine(item.ToString());
            }
            Clipboard.SetText(sb.ToString());
        }
        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyAllItemsToClipboard(listBox);
            }
        }
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyAllItemsToClipboard(listBox1);
            }
        }


        public class token
        {
            public string type { get; set; }
            public string value { get; set; }
            public token(string t, string v)
            {
                this.type = t;
                this.value = v;
            }

            public override string ToString()
            {
                return string.Concat(new string[]
                {
                    "(",
                    this.type,
                    ",",
                    this.value,
                    ")"
                });
            }
        }
        

        private token tokenizer()
        {
            int state = 0;
            string word = "";

            while (sourceProgram[currentPointer] != '#')
            {
                // Skip whitespace (space, tab, newline, etc.)
                if (char.IsWhiteSpace(sourceProgram[currentPointer]))
                {
                    currentPointer++;
                    continue;
                }

                if (state == 0)
                {
                    // Handling single character tokens and state transitions
                    if (sourceProgram[currentPointer] == ';')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("semiColon", word);
                    }

                    if (sourceProgram[currentPointer] == 'i')
                    {
                        state = 200;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == ',')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("comma", word);
                    }

                    if (sourceProgram[currentPointer] == '$')
                    {
                        state = 400;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == ':')
                    {
                        state = 500;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == '(')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("leftP", word);
                    }

                    if (sourceProgram[currentPointer] == ')')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("rightP", word);
                    }

                    if (sourceProgram[currentPointer] == 't')
                    {
                        state = 800;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == 'e')
                    {
                        state = 900;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == 'f')
                    {
                        state = 1900;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == 'w')
                    {
                        state = 1000;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == 'd')
                    {
                        state = 1100;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == '+')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("opPlus", word);
                    }

                    if (sourceProgram[currentPointer] == '-')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("opMinus", word);
                    }

                    if (sourceProgram[currentPointer] == '*')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("opTime", word);
                    }

                    
                    if (sourceProgram[currentPointer] == '/')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("opDiv", word);
                    }

                    if (sourceProgram[currentPointer] == '%')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        return new token("opMod", word);
                    }

                    if (sourceProgram[currentPointer] == '<')
                    {
                        state = 1400;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == '>')
                    {
                        state = 1500;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == '!')
                    {
                        state = 1600;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == 'b')
                    {
                        state = 1700;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] >= '0' && sourceProgram[currentPointer] <= '9')
                    {
                        state = 1800;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                // Handling keyword 'int'
                if (state == 200)
                {
                    if (sourceProgram[currentPointer] == 'n')
                    {
                        state = 201;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                    if (sourceProgram[currentPointer] == 'f')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_if", word);
                    }
                }

                if (state == 201)
                {
                    if (sourceProgram[currentPointer] == 't')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_int", word);
                    }
                }

                // Handling identifiers
                if (state == 400)
                {
                    if (sourceProgram[currentPointer] >= 'a' && sourceProgram[currentPointer] <= 'z')
                    {
                        word += sourceProgram[currentPointer];
                        state = 401;
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 401)
                {
                    if ((sourceProgram[currentPointer] >= 'a' &&
                         sourceProgram[currentPointer] <= 'z') ||
                        (sourceProgram[currentPointer] >= '0' &&
                         sourceProgram[currentPointer] <= '9'))
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }

                    if (sourceProgram[currentPointer] == '$')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("identifier", word);
                    }
                }

                if (state == 500)
                {
                    if (sourceProgram[currentPointer] == '=')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("assign", word);
                    }
                    else
                    {
                        print("error: Expected '=' after ':' for assignment");
                        return null;
                    }
                }

                // Handling keyword 'then'
                if (state == 800)
                {
                    if (sourceProgram[currentPointer] == 'h')
                    {
                        state = 801;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 801)
                {
                    if (sourceProgram[currentPointer] == 'e')
                    {
                        state = 802;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 802)
                {
                    if (sourceProgram[currentPointer] == 'n')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_then", word);
                    }
                }

                // Handling keyword 'else' and 'end'
                if (state == 900)
                {
                    if (sourceProgram[currentPointer] == 'l')
                    {
                        state = 901;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                    if (sourceProgram[currentPointer] == 'n')
                    {
                        state = 910;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 901)
                {
                    if (sourceProgram[currentPointer] == 's')
                    {
                        state = 902;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 902)
                {
                    if (sourceProgram[currentPointer] == 'e')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_else", word);
                    }
                }

                if (state == 910)
                {
                    if (sourceProgram[currentPointer] == 'd')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_end", word);
                    }
                }

                // Handling keyword 'while'
                if (state == 1000)
                {
                    if (sourceProgram[currentPointer] == 'h')
                    {
                        state = 1001;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1001)
                {
                    if (sourceProgram[currentPointer] == 'i')
                    {
                        state = 1002;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1002)
                {
                    if (sourceProgram[currentPointer] == 'l')
                    {
                        state = 1003;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1003)
                {
                    if (sourceProgram[currentPointer] == 'e')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_while", word);
                    }
                }

                // Handling keyword 'do'
                if (state == 1100)
                {
                    if (sourceProgram[currentPointer] == 'o')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_do", word);
                    }
                }

                // Handling relational operators
                if (state == 1400)
                {
                    if (sourceProgram[currentPointer] == '=')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("log_op", word);
                    }
                    else
                    {
                        state = 0;
                        return new token("log_op", word);
                    }
                }

                if (state == 1500)
                {
                    if (sourceProgram[currentPointer] == '=')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("log_op", word);
                    }
                    else
                    {
                        state = 0;
                        return new token("log_op", word);
                    }
                }

                if (state == 1600)
                {
                    if (sourceProgram[currentPointer] == '=')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("log_op", word);
                    }
                    else
                    {
                        state = 0;
                        return new token("log_op", word);
                    }
                }

                // Handling keyword 'begin'
                if (state == 1700)
                {
                    if (sourceProgram[currentPointer] == 'e')
                    {
                        state = 1701;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1701)
                {
                    if (sourceProgram[currentPointer] == 'g')
                    {
                        state = 1702;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1702)
                {
                    if (sourceProgram[currentPointer] == 'i')
                    {
                        state = 1703;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1703)
                {
                    if (sourceProgram[currentPointer] == 'n')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_begin", word);
                    }
                }

                // Handling numeric literals starting with digits
                if (state == 1800)
                {
                    if (sourceProgram[currentPointer] >= '0' && sourceProgram[currentPointer] <= '9')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                    else
                    {
                        state = 0;
                        return new token("integer", word);
                    }
                }

                // Handling keyword 'for'
                if (state == 1900)
                {
                    if (sourceProgram[currentPointer] == 'o')
                    {
                        state = 1901;
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        continue;
                    }
                }

                if (state == 1901)
                {
                    if (sourceProgram[currentPointer] == 'r')
                    {
                        word += sourceProgram[currentPointer];
                        currentPointer++;
                        state = 0;
                        return new token("kw_for", word);
                    }
                }

                // Error handling for unexpected characters
                print("Error(@position" + currentPointer.ToString() + "): Unexpected character or not defined yet '" + sourceProgram[currentPointer].ToString() + "'");
                currentPointer++;
            }

            return new token("#", "#"); // Return termination token when end of input is reached
        }



        public class Identifier
        {
            public string name { get; set; }
            public string type { get; set; }
            public string value { get; set; }

            public Identifier(string n)
            {
                this.name = n;
                this.type = "";
                this.value = "";
            }

            public override string ToString()
            {
                return $" {this.name} | [{this.type}] | [{this.value}]";
            }
        }

        public class IdentifierTable
        {
            private List<Identifier> list;

            public IdentifierTable()
            {
                list = new List<Identifier>();
            }
            

            public Identifier getIdentifierByName(string name)
            {
                foreach (Identifier t in list)
                {
                    if (t.name == name) return t;
                }
                return null;
            }

            public Boolean add(string name)
            {
                Identifier t = getIdentifierByName(name);
                if (t != null) return false;
                list.Add(new Identifier(name));
                return true;
            }

            public Boolean updateTypeByName(string name, string type)
            {
                Identifier t = getIdentifierByName(name);
                if (t == null) return false;
                t.type = type;
                return true;
            }

            public Boolean updateValueByName(string name, string value)
            {
                Identifier t = getIdentifierByName(name);
                if (t == null) return false;
                t.value = value;
                return true;
            }

            public void dump(ListBox lb)
            {
                lb.Items.Add("<--------- Identifier Table --------->");
                lb.Items.Add(string.Format("{0,-12} | {1,-10} | {2,-10}", "Name", "Type", "Value"));
                lb.Items.Add(new string('-', 36));
                foreach (Identifier t in list)
                {
                    lb.Items.Add(string.Format("{0,-12} | {1,-10} | {2,-10}", t.name, t.type, t.value));
                }
                lb.Items.Add(new string('-', 36));
            }
        }

        public class TemporaryVariableTable
        {
            private List<Identifier> list;

            public TemporaryVariableTable()
            {
                list = new List<Identifier>();
            }

            public Identifier CreateNewTempVar()
            {
                int index = list.Count;
                Identifier t = new Identifier($"T{index}");
                list.Add(t);
                return t;
            }


            public void dump(ListBox lb)
            {
                lb.Items.Add("<---- Temp Variable Table ---------->");
                lb.Items.Add(string.Format("{0,-12} | {1,-10} | {2,-10}", "Name", "Type", "Value"));
                lb.Items.Add(new string('-', 36));
                foreach (Identifier t in list)
                {
                    lb.Items.Add(string.Format("{0,-12} | {1,-10} | {2,-10}", t.name, t.type, t.value));
                }
                lb.Items.Add(new string('-', 36));
            }

        }

        public class Quadruple
        {
            public string Op1 { get; set; }
            public string Op2 { get; set; }
            public string Op3 { get; set; }
            public string Op4 { get; set; }

            public Quadruple(string op, string opr1, string opr2, string result)
            {
                this.Op1 = op;
                this.Op2 = opr1;
                this.Op3 = opr2;
                this.Op4 = result;
            }

            public override string ToString()
            {
                return $"({this.Op1}, {this.Op2}, {this.Op3}, {this.Op4})";
            }
        }

        public class QuadrupleTable
        {
            private List<Quadruple> list;

            public int NXQ
            {
                get { return list.Count; }
            }

            public QuadrupleTable()
            {
                list = new List<Quadruple>();
            }

            public bool Add(string op, string opr1, string opr2, string result)
            {
                this.list.Add(new Quadruple(op, opr1, opr2, result));
                return true;
            }

            public Boolean BackPatch(int index, string result)
        {
            if (index >= 0 && index < list.Count) // Ensuring the index is within valid range
            {
                list[index].Op4 = result; // Update the fourth element (result) of the specified Quadruple
                return true;
            }
            return false;

        }

        public Quadruple GetAt(int index)
        {
            return list[index];
        }

        public void RemoveRange(int start, int count)
        {
            list.RemoveRange(start, count);
        }

        public void AddQuadruple(Quadruple q)
        {
            list.Add(q);
        }


        public void dump(ListBox lb)
            {
                lb.Items.Add("<-------- Mid-Code Table ------------->");
                lb.Items.Add(string.Format("{0,-3} | {1,-4} | {2,-6} | {3,-6} | {4,-6}", "Idx", "Op", "Opr1", "Opr2", "Result"));
                lb.Items.Add(new string('-', 38));
                for (int i = 0; i < list.Count; i++)
                {
                    Quadruple q = list[i];
                    lb.Items.Add(string.Format("{0,-3} | {1,-4} | {2,-6} | {3,-6} | {4,-6}", i, q.Op1, q.Op2, q.Op3, q.Op4));
                }
                lb.Items.Add(new string('-', 38));
            }
        }

        private void debug(string s)
        {
            listBox1.Items.Add(s);
        }

        private void HandleParseClick(object sender, EventArgs e)
        {
            

            sourceProgram = textBox1.Text + "#";
            listBox.Items.Clear();
            listBox1.Items.Clear();
            Identifiers = new IdentifierTable();
            tempVars = new TemporaryVariableTable();
            midCodes = new QuadrupleTable();

            debug(sourceProgram);
            currentPointer = 0;
            currentToken = tokenizer();
            print(currentToken.ToString());

            // Directly use the result of parseProgram in the if condition
            if (parseProgram())
            {
                print("end..");
            }
            else
            {
                print("error...");
            }
            midCodes.dump(listBox);
            Identifiers.dump(this.listBox1);
            tempVars.dump(this.listBox1);
        }

        public bool match(string expectedType)
        {
            if (currentToken.type == expectedType)
            {
                debug(string.Concat(new string[]
                {
            "expec ",
            expectedType,
            ", currentToken ",
            currentToken.type,
            ", matched."
                }));
                currentToken = tokenizer();
                print(currentToken.ToString());
                return true;
            }
            else
            {
                print(string.Concat(new string[]
                {
            "Error: expec ",
            expectedType,
            ", got ",
            currentToken.ToString(),
            "."
                }));
                return false;
            }
        }
  
        public bool parseProgram()
        {
            print("< program > → <variable declaration section> ; <statements section>");
            if (!parseDeclarationSection())
            {
                print("error: Fail to parse <variable declaration section>");
                return false;
            }
            if (!match("semiColon"))
            {
                return false;
            }
            if (!parseStatementsSection())
            {
                print("error: Fail to parse <statements section>");
                return false;
            }
            debug("[< program > → <variable declaration section> ; <statements section>]end");
            return true;
        }
       
        public bool parseDeclarationSection()
        {
            print("< variable declaration section > → int <variable list>");
            string type = currentToken.value;
            if (!match("kw_int"))
            {
                return false;
            }
            if (!parseVariableList(type))
            {
                print("error: Fail to parse <variable list>");
                return false;
            }
            debug("[< variable declaration section > → int <variable list>]end");
            return true;
        }

		public bool parseVariableList(string type)
        {
            print("< variable list > → identifier A");
            string name = currentToken.value;
            if (!match("identifier"))
            {
                return false;
            }
            if (!Identifiers.add(name))
            {
                print("error: Fail to add identifier " + name);
                return false;
            }
            if (!Identifiers.updateTypeByName(name, type))
            {
                print("error: Fail to update identifier " + name + " with type " + type);
                return false;
            }
            if (!parseA(type))
            {
                print("error: Fail to parse A");
                return false;
            }
            debug("[< variable list > → identifier A]end");
            return true;
        }

        public bool parseA(string type)
        {
            if (currentToken.type == "comma")
            {
                print("A → , identifier A");
                if (!match("comma"))
                {
                    return false;
                }
                string name = currentToken.value;
                if (!match("identifier"))
                {
                    return false;
                }
                if (!Identifiers.add(name))
                {
                    print("error: Fail to add identifier " + name);
                    return false;
                }
                if (!Identifiers.updateTypeByName(name, type))
                {
                    print("error: Fail to update identifier " + name + " with type " + type);
                    return false;
                }
                if (!parseA(type))
                {
                    print("error: Fail to parse A");
                    return false;
                }
                debug("[A → , identifier A]end");
                return true;
            }
            else
            {
                if (currentToken.type == "semiColon")
                {
                    print("A →ε");
                    return true;
                }
                print("can't choose production for parseA with " + currentToken.ToString());
                return false;
            }
        }

        public bool parseStatementsSection()
        {
            print("< statements section > → <statement>; B");
            if (!parseStatement())
            {
                print("error: Fail to parse <statement>");
                return false;
            }
            if (!match("semiColon"))
            {
                return false;
            }
            if (!parseB())
            {
                    print("error: Fail to parse B");
                return false;
            }
            debug("[< statements section > → <statement>; B]end");
            return true;
        }

        public bool parseB()
        {
            if(currentToken.type == "identifier" || currentToken.type == "kw_if" || currentToken.type == "kw_while" || currentToken.type == "kw_for") 
            {
                print("B → <statement>; B");
                if (!parseStatement())
                {
                    print("error: Fail to parse <statement>");
                    return false;
                }
                if (!match("semiColon"))
                {
                    return false;
                }
                if (!parseB())
                {
                    print("error: Fail to parse B");
                    return false;
                }
                debug("[B → <statement>; B]end");
                return true;
            }
            else
            {
                if (currentToken.type == "#" || currentToken.type == "kw_end")
                {
                    print("B →ε");
                    return true;
                }
                print("can't choose production for parseB with " + currentToken.ToString());
                return false;
            }
        }
       
        public bool parseStatement()
        {
            if (currentToken.type == "identifier")
            {
                print("< statement > → <assignment statement>");
                if (!parseAssignmentStatement())
                {
                    print("error: Fail to parse <assignment statement>");
                    return false;
                }
                debug("[< statement > → <assignment statement>]end");
                return true;
            }
            else if (currentToken.type == "kw_if")
            {
                print("< statement > →<conditional statement>");
                if (!parseConditionalStatement())
                {
                    print("error: Fail to parse <conditional statement>");
                    return false;
                }
                debug("[< statement > →<conditional statement>]end");
                return true;
            }
            else
            {
                if (!(currentToken.type == "kw_while" || currentToken.type == "kw_for"))
                {
                    print("error: can't choose production for parseStatement with " + currentToken.ToString());
                    return false;
                }
                print("< statement > →<iteration statement>");
                if (!parseIterationStatement())
                {
                    print("error: Fail to parse <iteration statement>");
                    return false;
                }
                debug("[< statement > →<iteration statement>]end");
                return true;
            }
        }

        public bool parseAssignmentStatement()
        {
            print("<assignment statement> → identifier = <expression>");

            string name = currentToken.value;
            if (!match("identifier"))
            {
                print("error: Expected 'identifier' but got " + currentToken.value);
                return false;
            }

            if (!match("assign"))
            {
                print("error: Expected '=' after identifier.");
                return false;
            }

            Identifier E = parseExpression();
            if (E == null)
            {
                print("error: Failed to parse <expression>");
                return false;
            }

            if (!Identifiers.updateValueByName(name, E.value))
            {
                print($"error: Cannot update identifier '{name}' with value '{E.value}'");
                return false;
            }

            midCodes.Add("=", E.name, "null", name);
            debug("<assignment statement> → identifier = <expression> end");
            return true;
        }

        public Identifier parseExpression()
        {
            print("<expression> → <item> C");

            Identifier E1 = parseItem();
            if (E1 == null)
            {
                print("error: Failed to parse <item>");
                return null;
            }

            Identifier result = parseC(E1);
            if (result == null)
            {
                print("error: Failed to parse C");
                return null;
            }

            debug("[<expression> → <item> C] end");
            return result;
        }

        public Identifier parseItem()
        {
            print("<item> → <factor> D");

            Identifier E1 = parseFactor();
            if (E1 == null) { print("error: Failed to parse <factor>"); return null; }

            Identifier result = parseD(E1);
            if (result == null) { print("error: Failed to parse D"); return null; }

            debug("[<item> → <factor> D] end");
            return result;
        }



        // Parses expressions with + and - operators
        public Identifier parseC(Identifier E1)
        {
            while (currentToken.type == "opPlus" || currentToken.type == "opMinus")
            {
                string op = currentToken.type == "opPlus" ? "+" : "-";
                print("C -> <item> C with " + op);

                if (!match(currentToken.type)) { print("error: Expected '" + op + "'"); return null; }

                Identifier E2 = parseItem();
                if (E2 == null) { print("error: Failed to parse second item in expression"); return null; }

                Identifier T = tempVars.CreateNewTempVar();
                T.type = E1.type;
                try
                {
                    if (op == "+") T.value = (Convert.ToInt32(E1.value) + Convert.ToInt32(E2.value)).ToString();
                    else T.value = (Convert.ToInt32(E1.value) - Convert.ToInt32(E2.value)).ToString();
                }
                catch (Exception ex) { print("error: " + ex.Message); return null; }

                midCodes.Add(op, E1.name, E2.name, T.name);
                E1 = T; // Update E1 for next iteration
            }

            print("C -> ε");
            return E1;
        }

        // Parses expressions with * operator
        public Identifier parseD(Identifier E1)
        {
            if (
                currentToken.type == "opTime" ||
                currentToken.type == "opDiv" ||
                currentToken.type == "opMod"
               )
            {
                string op = "";

                if (currentToken.type == "opTime")
                    op = "*";
                else if (currentToken.type == "opDiv")
                    op = "/";
                else if (currentToken.type == "opMod")
                    op = "%";

                print("D -> " + op + " <factor> D");

                if (!match(currentToken.type))
                    return null;

                Identifier E2 = parseFactor();

                if (E2 == null)
                {
                    print("error: Failed to parse factor after " + op);
                    return null;
                }

                Identifier T = tempVars.CreateNewTempVar();
                T.type = E1.type;

                try
                {
                    int v1 = Convert.ToInt32(E1.value);
                    int v2 = Convert.ToInt32(E2.value);

                    switch (op)
                    {
                        case "*":
                            T.value = (v1 * v2).ToString();
                            break;

                        case "/":
                            if (v2 == 0)
                            {
                                print("error: Division by zero");
                                return null;
                            }
                            T.value = (v1 / v2).ToString();
                            break;

                        case "%":
                            if (v2 == 0)
                            {
                                print("error: Modulo by zero");
                                return null;
                            }
                            T.value = (v1 % v2).ToString();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    print("error: " + ex.Message);
                    return null;
                }

                midCodes.Add(op, E1.name, E2.name, T.name);

                return parseD(T);
            }
            else if (
                currentToken.type == "semiColon" ||
                currentToken.type == "rightP" ||
                currentToken.type == "log_op" ||
                currentToken.type == "opPlus" ||
                currentToken.type == "opMinus"
                    )
            {
                print("D -> ε");
                return E1;
            }

            print("error: Can't choose production for parseD with "
                  + currentToken.ToString());

            return null;
        }

        public Identifier parseFactor()
        {
            if (currentToken.type == "identifier")
            {
                print("< factor > → identifier");

                string name = currentToken.value;
                if (!match("identifier"))
                {
                    print("error: Expected 'identifier' but found " + currentToken.value);
                    return null;
                }

                Identifier F = Identifiers.getIdentifierByName(name);
                if (F == null)
                {
                    print("error: Identifier '" + name + "' not found");
                    return null;
                }
                if (string.IsNullOrEmpty(F.value))
                {
                    print("error: The value of identifier '" + name + "' is empty");
                    return null;
                }
                debug("[< factor > → identifier] end");
                return F;
            }
            else if (currentToken.type == "integer")
            {
                print("< factor > → integer");

                string value = currentToken.value;
                if (!match("integer"))
                {
                    print("error: Expected 'integer' but found " + currentToken.value);
                    return null;
                }

                Identifier F = new Identifier(value);
                F.type = "int";
                F.value = value;

                debug("[< factor > → integer] end");
                return F;
            }
            else if (currentToken.type == "leftP")
            {
                print("< factor > → ( < expression > )");

                if (!match("leftP"))
                {
                    print("error: Expected '(' but found " + currentToken.value);
                    return null;
                }

                Identifier E = parseExpression();
                if (E == null)
                {
                    print("error: Failed to parse < expression >");
                    return null;
                }

                if (!match("rightP"))
                {
                    print("error: Expected ')' but found " + currentToken.value);
                    return null;
                }

                debug("[< factor > → ( < expression > )] end");
                return E;
            }

            print($"error: Can’t choose production for parseFactor with {currentToken.ToString()}");
            return null;
        }

        public bool parseConditionalStatement()
        {
            print("< conditional statement > → if （< condition >） then <nested statement> ; else < nested statement > ");
            if (!match("kw_if"))
            {
                return false;
            }
            if (!match("leftP"))
            {
                return false;
            }
            Identifier T = parseCondition();
            if ( T == null)
            {
                print("error: Fail to < condition >");
                return false;
            }
            midCodes.Add("jnz", T.name, "null", (midCodes.NXQ + 2).ToString());
            int falseIndex = midCodes.NXQ;
            midCodes.Add("j", "null", "null", "0");
            if (!match("rightP"))
            {
                return false;
            }
            if (!match("kw_then"))
            {
                return false;
            }
            if (!parseNestedStatement())
            {
                return false;
            }
            int exitIndex = midCodes.NXQ;
            midCodes.Add("j", "null", "null", "0");
            midCodes.BackPatch(falseIndex, midCodes.NXQ.ToString());
            if (!match("semiColon"))
            {
                return false;
            }
            if (!match("kw_else"))
            {
                return false;
            }
            if (!parseNestedStatement())
            {
                return false;
            }
            midCodes.BackPatch(exitIndex, midCodes.NXQ.ToString());
            debug("[< conditional statement > → if （< condition >） then <nested statement> ; else < nested statement > ");
            return true;
        }

        public Identifier parseCondition()
        {
            print("<expression> logical_operator <expression>");

            // Parse the first expression
            Identifier E1 = parseExpression();
            if (E1 == null)
            {
                print("error: Failed to parse the first expression");
                return null;
            }

            // Get the logical operator
            string op = currentToken.value;
            if (!match("log_op"))
            {
                print("error: Expected a logical operator but found " + currentToken.value);
                return null;
            }

            // Parse the second expression
            Identifier E2 = parseExpression();
            if (E2 == null)
            {
                print("error: Failed to parse the second expression");
                return null;
            }

            // Create a temporary variable for the result of the condition
            Identifier T = tempVars.CreateNewTempVar();
            T.type = "bool";

            // Evaluate the logical operation and store the result
            if (string.IsNullOrEmpty(E1.value) || string.IsNullOrEmpty(E2.value))
            {
                print($"error: Value(s) empty - E1: {E1.value}, E2: {E2.value}");
                return null;
            }

            // Handling different operators
            try
            {
                switch (op)
                {
                    case "==":
                        T.value = (E1.value == E2.value).ToString();
                        break;
                    case "!=":
                        T.value = (E1.value != E2.value).ToString();
                        break;
                    case "<":
                        T.value = (Convert.ToInt32(E1.value) < Convert.ToInt32(E2.value)).ToString();
                        break;
                    case "<=":
                        T.value = (Convert.ToInt32(E1.value) <= Convert.ToInt32(E2.value)).ToString();
                        break;
                    case ">":
                        T.value = (Convert.ToInt32(E1.value) > Convert.ToInt32(E2.value)).ToString();
                        break;
                    case ">=":
                        T.value = (Convert.ToInt32(E1.value) >= Convert.ToInt32(E2.value)).ToString();
                        break;
                    default:
                        print("error: Unknown logical operator " + op);
                        return null;
                }
            }
            catch (Exception ex)
            {
                T.value = "false";  // Defaulting to false in case of any exception
                print($"error: Failed to evaluate condition due to {ex.Message}");
            }

            // Add the result of the evaluation to the middle code table
            midCodes.Add(op, E1.name, E2.name, T.name);
            debug($"[<expression> {op} <expression>] end");

            return T;
        }

        public bool parseNestedStatement()
        {
            if (currentToken.type == "identifier" || currentToken.type == "kw_if" || currentToken.type == "kw_while" || currentToken.type == "kw_for")
            {
                print("< nested statement > → <statement>");
                if (!parseStatement())
                {
                    return false;
                }
                debug("[< nested statement > → <statement>]end");
                return true;
            }
            else if (currentToken.type == "kw_begin")
            {
                print("< nested statement > → <compound statement>");
                if (!parseCompoundStatement())
                {
                    return false;
                }
                debug("[< nested statement > → <compound statement>]end");
                return true;
            }
            else
            {
                print("can't choose production for < nested statement > with " + currentToken.ToString());
                return false;
            }
        }    

        public bool parseCompoundStatement()
        {
            print("< compound statement > → begin < statements section > end");

            // Check if 'begin' keyword matches
            if (!match("kw_begin"))
            {
                return false;
            }

            // Attempt to parse the statements section
            if (!parseStatementsSection())
            {
                return false;
            }

            // Check if 'end' keyword matches
            if (!match("kw_end"))
            {
                return false;
            }

            // If all checks pass, debug and return true
            debug("[< compound statement > → begin < statements section > end]end");
            return true;
        }

        public bool parseIterationStatement()
        {
            bool isForLoop = false;

            if (currentToken.type == "kw_while")
            {
                print("while （< condition >） do < nested statement >");

                if (!match("kw_while"))
                {
                    return false;
                }
            }
            else if (currentToken.type == "kw_for")
            {
                isForLoop = true;

                print("for ( assignment ; condition ; assignment ) do < nested statement >");

                if (!match("kw_for"))
                {
                    return false;
                }
            }
            else
            {
                print("error: Expected while or for.");
                return false;
            }
            if (!match("leftP"))
            {
                print("error: Expected '(' after 'while'.");
                return false;
            }
            if (isForLoop && currentToken.type == "identifier")
            {
                if (!parseAssignmentStatement())
                {
                    print("error: Failed to parse initialization assignment.");
                    return false;
                }

                if (!match("semiColon"))
                {
                    print("error: Expected ';' after initialization.");
                    return false;
                }
            }
            int next = midCodes.NXQ;
            Identifier T = parseCondition();
            if ( T == null)
            {
                print("error: Fail to < condition >");
                return false;
            }
            List<Quadruple> savedUpdateCode = new List<Quadruple>();

            if (isForLoop && currentToken.type == "semiColon")
            {
                match("semiColon");

                int updateStart = midCodes.NXQ;

                if (!parseAssignmentStatement())
                {
                    print("error: Failed to parse update assignment.");
                    return false;
                }

                int updateCount = midCodes.NXQ - updateStart;

                for (int i = 0; i < updateCount; i++)
                {
                    savedUpdateCode.Add(midCodes.GetAt(updateStart + i));
                }

                midCodes.RemoveRange(updateStart, updateCount);
            }
            midCodes.Add("jnz", T.name, "null", (midCodes.NXQ + 2).ToString());
            int falseIndex = midCodes.NXQ;
            midCodes.Add("j", "null", "null", "0");
            if (!match("rightP"))
            {
                return false;
            }
            if (!match("kw_do"))
            {
                return false;
            }
            if (!parseNestedStatement())
            {
                print("error: Failed to parse the nested statement.");
                return false;
            }

            foreach (Quadruple q in savedUpdateCode)
            {
                midCodes.AddQuadruple(q);
            }

            midCodes.Add("j", "null", "null", next.ToString());
            

            midCodes.BackPatch(falseIndex, midCodes.NXQ.ToString());
            debug("[while （< condition >） do < nested statement >]end");
            return true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

