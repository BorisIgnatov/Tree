using System;
using System.Collections.Generic;
using System.Linq;
namespace graphC
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,string[]> dict = new Dictionary<string, string[]>();
            dict.Add("A",new string[] {"B","C"});
            dict.Add("B", new string[] { });    
            dict.Add("C", new string[] {"E"});
            dict.Add("D", new string[] { });
            dict.Add("E", new string[] { });
            Tree tree = new Tree(dict);
            Console.WriteLine(tree.findNode("B"));
            foreach(Node node in tree.findAncestors("D"))
            {
                Console.WriteLine(node);
            }
            Console.WriteLine(tree.findLevel("D"));
            foreach(Node node in tree.findDescendants("A"))
            {
                Console.WriteLine(node);
            }
            Console.WriteLine(tree.findNode("D"));
            Console.WriteLine(tree);
        }
        class Tree
        {
            private Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            private List<Node> nodes = new List<Node>();
            private List<Node> listOfNodes = new List<Node>();
            public Tree(Dictionary<string, string[]> dict)
            {
                this.dict = dict;
                foreach(KeyValuePair<string, string[]> i in this.dict)
                {
                    this.nodes.Add(new Node(i.Key));
                }

                for(int i = 0; i < this.nodes.Count; i++)
                {
                    string[] ch = this.dict[this.nodes[i].Name];
                    List<Node> q = new List<Node>();
                    for (int j = 0; j < ch.Length; j++)
                    {
                        var q2 = this.nodes.Where(x => x.Name == ch[j]);
                        q2.ElementAt(0).Parent = this.nodes[i];
                        q.Add(q2.ToList()[0]);
                    }
                    this.nodes[i].Childrens = q; 
                }
            }


            public List<Node> findAncestors(string node)
            {

                Node n = this.findNode(node);
                List<Node> listOfNodes = new List<Node>();
                while (n.Parent != null)
                {
                    listOfNodes.Add(n.Parent);
                    n = n.Parent;
                }
                return listOfNodes;
                /*for(int i = 0; i < listOfNodes.Count; i++)
                {
                    Console.WriteLine(listOfNodes[i]);
                }*/
            }

            public List<Node> findDescendants(string node)
            {
                Node n = this.findNode(node);
                foreach(Node i in n.Childrens)
                {
                    int t = n.Childrens.Count;
                    this.listOfNodes.Add(i);
                    if(i.Childrens != null)
                    {
                        this.findDescendants(i.Name);
                        t--;
                    }
                    if (t==0)
                    {
                        return this.listOfNodes;
                    }
                }
                return this.listOfNodes;
            }


            public int findLevel(string node)
            {
                int level = 0;
                Node n = this.findNode(node);
                return this.findAncestors(node).Count;
            }


            public Node findNode(string node)
            {
                Node n = this.nodes.Where(x => x.Name == node).ToList().ElementAt(0);
                return n;
            }


            public override string ToString()
            {
                string s = "";
                foreach(Node i in this.nodes)
                {
                    s += i.ToString()+"\n";
                }
                return s;
            }
        }


        class Node
        {
            private string name;
            private Node parent;
            private List<Node> childrens;

            public Node(string name, Node parent=null, List<Node> childrens=null)
            {
                this.name = name;
                this.parent = parent;
                this.childrens = childrens;
            }

            public string Name { get => this.name; set => this.name = value; }
            public Node Parent { get => this.parent; set => this.parent = value; }
            public List<Node> Childrens { get => this.childrens; set => this.childrens = value; }

            public override string ToString()
            {
                string s = this.name + ": ";
                try
                {
                    throw new Exception();
                    s += " parent - " + this.Parent.Name;
                }catch(Exception)
                {
                    s += " parent - ";
                }
                s += " childrens - ";
                if(this.Childrens != null)
                {
                    foreach (Node i in this.Childrens)
                    {
                        s += i.Name;
                    }
                }
                s += "; ";
                return s;
            }
        }
    }
}
