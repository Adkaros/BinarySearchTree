using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree bst = new BinarySearchTree();
            
            bst.Insert(33);
            bst.Insert(55);
            bst.Insert(73);
            bst.Insert(22);
            bst.Insert(11);
            bst.Insert(1);
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(8);
            bst.Insert(2);
            bst.Insert(79);
            bst.Insert(77);
            bst.Insert(65);

            bst.Remove(bst[5]);
            
            bst.Search(2);

            Console.WriteLine("-----\nInorder Traversal\n-----");

            List<BinarySearchTree.Node> nodes = bst.InorderTraversal();
            foreach (BinarySearchTree.Node n in nodes)
            {
                Console.WriteLine(n.data);
            }

            Console.ReadKey();
        }        
    }

    class BinarySearchTree
    {
        Node root = null;
        int count = 0;

        public void Insert(int num)
        {
            if (root == null)
            {
                root = new Node(num);
                count++;
            }
            else
            {
                Node current = root;

                while (true)
                {
                    if (!IsGreaterThan(num, current.data))
                    {
                        if (current.left != null)
                        {
                            current = current.left;
                        }
                        else
                        {
                            current.left = new Node(num);
                            current.left.parent = current;
                            current = current.left;
                            break;
                        }
                    }
                    else
                    {
                        if (current.right != null)
                        {
                            current = current.right;
                        }
                        else
                        {
                            current.right = new Node(num);
                            current.right.parent = current;
                            current = current.right;
                            break;
                        }
                    }
                }
                count++;
            }
        }

        public void Remove(Node node)
        {
            if (node == null)
            {
                return;
            }
            else
            {
                Node toDelete = node;

                if (node.data == toDelete.data)
                {
                    if (toDelete.left == null && toDelete.right == null)
                    {
                        toDelete = null;
                    }
                    else if (toDelete.left == null && toDelete.right != null ||
                             toDelete.left != null && toDelete.right == null)
                    {
                        Node tempChild = (toDelete.left == null) ? toDelete.right : toDelete.left;
                        Node tempParent = toDelete.parent;

                        if (toDelete == tempParent.left)
                        {
                            tempParent.left = tempChild;
                        }
                        else
                        {
                            tempParent.right = tempChild;
                        }
                    }
                    else if (toDelete.left != null && toDelete.right != null)
                    {
                        Node successor = GetSuccessor(toDelete);

                        Console.WriteLine("Deleting node with 2 children");

                        if (successor.right != null)
                        {
                            Node tempChild = successor.right;
                            successor.parent.left = tempChild;
                        }

                        toDelete.data = successor.data;
                        successor = null;
                    }
                }
                count--;           
            }
        }

        public Node Search(int num)
        {
            if (root == null)
            {
                return null;
            }
            else
            {
                Node current = root;
                Console.WriteLine("Search Path");

                while (true)
                {
                    Console.WriteLine(current.data);

                    if (num == current.data)
                    {
                        return current;
                    }
                    else if (!IsGreaterThan(num, current.data))
                    {
                        if (current.left != null)
                        {
                            current = current.left;
                        }
                    }
                    else
                    {
                        if (current.right != null)
                        {
                            current = current.right;
                        }
                    }
                }
            }
        }

        public Node this[int num]
        {
            get
            {
                if (root == null)
                {
                    return null;
                }
                else
                {
                    Node current = root;
                    while (true)
                    {
                        if (num == current.data)
                        {
                            return current;
                        }
                        else if (!IsGreaterThan(num, current.data))
                        {
                            if (current.left != null)
                            {
                                current = current.left;
                            }
                        }
                        else
                        {
                            if (current.right != null)
                            {
                                current = current.right;
                            }
                        }
                    }
                }
            }
        }

        public Node GetPredecessor(Node node)
        {
            Node current = node;

            if (current != null)
            {
                if (current.left != null)
                {
                    return FindMaximum(current.left);
                }
                else
                {
                    Node tempParent = current.parent;
                    while ((tempParent != null) && (current == tempParent.left))
                    {
                        current = tempParent;
                        tempParent = tempParent.parent;
                    }

                    if (tempParent != null)
                        return tempParent;
                    else
                        Console.WriteLine("No predecessor found!");
                }
            }
            else
            {
                Console.WriteLine("Please enter the valid tree element!");
            }

            return null;
        }

        public Node GetSuccessor(Node node)
        {
            Node current = node;

            if (current != null)
            {
                if (current.right != null)
                {
                    return FindMinimum(current.right);                    
                }
                else
                {
                    Node tempParent = current.parent;

                    while ((tempParent != null) && (current == tempParent.right))
                    {
                        current = tempParent;
                        tempParent = tempParent.parent;
                    }

                    if (tempParent != null)
                        return tempParent;
                    else
                        Console.WriteLine("No successor found!");
                }
            }
            else
            {
                Console.WriteLine("Please enter the valid tree element!");
            }

            return null;
        }

        public Node FindMinimum(Node node)
        {
            Node current = node;
            if (current.left == null)
            {
                return current;
            }
            return FindMinimum(current.left);
        }

        public Node FindMaximum(Node node)
        {
            Node current = node;
            if (current.right == null)
            {
                return current;
            }
            return FindMaximum(current.right);
        }

        public List<Node> InorderTraversal()
        {
            List<Node> nodes = new List<Node>(count);

            nodes.Add(FindMinimum(root));

            for (int i = 0; i < count; i++)
            {
                nodes.Add(GetSuccessor(nodes[i]));
            }

            return nodes;
        }

        private bool IsGreaterThan(int num, int curr)
        {
            return (num > curr) ? true : false;
        }

        internal class Node
        {
            public Node parent, left, right;
            public int data;

            public Node(int data) { this.data = data; }
        }
    }
}
