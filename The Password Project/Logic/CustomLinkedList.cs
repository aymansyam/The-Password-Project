namespace The_Password_Project.Model
{
    public class CustomLinkedList
    {
        public Node Head { get; set; }
        public Node Iterator { get; set; }

        public void Add(object data)
        {
            if (Head == null)
            {
                Head = new Node(data);

                Head.Next = null;
            }
            else
            {
                Node toAdd = new Node(data);

                Node current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toAdd;
            }
        }

        public Node GetLast()
        {
            Node current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            return current;
        }
    }

    public class Node
    {
        public Node Next { get; set; }
        public object Data { get; set; }

        public Node(object t)
        {
            Data = t;
        }
    }
}