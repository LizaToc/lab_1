using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Queue
{
    class MyQueue : Queue<int>
    {
        public void Push(int NewValue) //+1
        {
            this.Enqueue(NewValue); //+1
        }
        public int Pop()
        {
            return this.Dequeue(); //+1+1
        }
        public int Get(ref ulong schet, int pos) //+1
        {
            schet++;
            schet += 2;
            for (int i = 0; i < pos; ++i) //+2
            {
                schet += 6;
                this.Push(Pop()); //1+2+1+2
                schet += 2;
            } //+2
            schet += 2;
            int result = this.Peek(); //+1+1
            schet += 2;
            for (int i = pos; i < this.Count; ++i) //+2
            {
                schet += 6;
                this.Push(Pop()); //1+2+1+2
                schet += 2;
            } //+2
            schet++;
            return result; //+1
        }
        public void Set(ref ulong schet, int pos, int newValue) //+2
        {
            schet += 2;
            schet += 2;
            for (int i = 0; i < pos; ++i) //+2
            {
                schet += 6;
                this.Push(Pop()); //1+2+1+2
                schet += 2;
            } //+2
            schet += 3;
            this.Push(newValue); //+1+2
            schet += 3;
            this.Pop(); //+1+2
            schet += 3;
            for (int i = pos; i < this.Count - 1; ++i) //+3
            {
                schet += 6;
                this.Push(Pop()); //1+2+1+2
                schet += 3;
            }  //+3
        }
        public void Print()
        {
            foreach (int item in this)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n---------");
        }

        public void Swap(ref ulong schet, int a, int b) //+2
        {
            schet += 2;
            int temp = this.Get(ref schet, a); //+1+1+8+8*this.Count
            schet += 2;
            this.Set(ref schet, a, this.Get(ref schet, b)); //+1+4-a+9*this.Count+1+8+8*this.Count
            schet += 2;
            this.Set(ref schet, b, temp); //+1+4-b+9*this.Count
            schet++;
        }

        public void QSort(ref ulong schet, int firstIndex = 0, int lastIndex = -1) //+2
        {
            schet += 3;
            if ((lastIndex == -1) || (firstIndex >= lastIndex)) //+3
            {
                schet++;
                return; //+1
            }
            int currentIndex = firstIndex; //+1
            schet++;
            schet += 3;
            for (int i = firstIndex + 1; i <= lastIndex; ++i) //+3
            {
                schet += 3;
                if (this.Get(ref schet, i) <= this.Get(ref schet, firstIndex)) //+1+8+8*this.Count+1+1+8+8*this.Count
                {
                    schet += 1;
                    this.Swap(ref schet, ++currentIndex, i); //+1+49+34*this.Count-currentIndex-i
                }
                schet += 2;
            } //+2
            schet++;
            this.Swap(ref schet, firstIndex, currentIndex); //+1+49+34*this.Count-firstIndex-currentIndex
            schet++;
            QSort(ref schet, firstIndex, currentIndex - 1);
            schet++;
            QSort(ref schet, currentIndex + 1, lastIndex);
        }
        public void QSort(ref ulong schet)
        {
            this.QSort(ref schet, 0, this.Count - 1);
        }
        public void fill(int l) //+1
        {
            var random = new Random(); //+1+1
            for (int i = 0; i < l; i++) //+2
            {
                this.Push(random.Next(-100, 101)); //+1+2+1+2
            } //+2
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyQueue myqueue = new MyQueue();
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 10; i++)
            {
                myqueue.Clear();
                myqueue.fill(300 * (i + 1));
                stopwatch.Restart();
                ulong schet = 0;
                myqueue.QSort(ref schet);
                stopwatch.Stop();
                Console.WriteLine($@"Номер сортировки: {i + 1}. 
        Количество отсортированных элементов: {myqueue.Count}. 
        Потрачено миллисекунд на выполнение: {stopwatch.ElapsedMilliseconds}. 
        Количество операций (N_op):  {schet}");
            }
            Console.WriteLine("Сортировки завершены. Нажмите любую клавишу, чтобы выйти из программы.");
            Console.ReadKey();
        }
    }
}
