using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.AST
{
    class Tree
    {
        public Node root;
        public Node left;
        public Node right;


        Tree(Node r)
        {
            this.root = r;
            this.right = null;
            this.left = null;
        }

        public Node Root
        {
            get { return root; }
            set { root = value; }
        }

        public Node Left
        {
            get { return left; }
            set { left = value; }
        }
        public Node Right
        {
            get { return right; }
            set { right = value; }
        }
    
    }
}
