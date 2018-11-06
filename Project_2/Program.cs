using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class karınca
    {
        
        public int life;
        public int seq;
        public karınca(int sıra)
        {
            this.seq = sıra;
            this.life = 3;
        }
    }
    class node
    {
        public karınca ant;
        public node next;
        public node prev;
        public cukur baglantı;
        public node(cukur dip)
        {
            ant = null;
            next = null;
            this.baglantı = dip;
            this.baglantı.bag = this;
        }
        public node()
        {
            ant = null;
            next = null;
        }
        public node(tuzak t)
        {
            ant = null;
            next = null;
            baglantı = t;
            t.bag = this;
        }
        public void yakala()
        {
            if (((tuzak)baglantı).kapan()==true)
            {
                baglantı = null;
            }
        }
    }
    class cukur
    {
        public node bag;
        node first;
        node last;
        int len;
        int max;
        public cukur(int size)
        {
            max = size;
            len = 0;
            first = null;
            last = null;
        }
        public bool push(node element)
        {
            if (isEmpty() == true)
            {
                first = element;
                last = element;
                len++;
                return true;
            }
            if (len+1<max || len+1==max)
            {
                element.next = last;
                last = element;
                len++;
                return true;
            }
            else
            {
                return false;
            }
        }
        public node pop()
        {
            if (len==0)
            {
                return null;
            }
            else
            {
                node temp = last;
                last = last.next;
                temp.next = null;
                len--;
                return temp;
            }
        }
        public bool isEmpty()
        {
            if (len==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isFull()
        {
            if (len == max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  bool dusme()
        {
            if (isFull() == false && bag.ant!=null)
            {
                node temp1 = new node();
                temp1.ant = bag.ant;
                this.push(temp1);
                this.bag.ant = null;
                return true;
            }
            else
            {
                return false;
            }
        }
       public void cıkma()
        {
            if (isFull() == true)
            {
                if (bag.ant == null && bag.next.ant != null)
                {
                    bag.ant = this.pop().ant;
                }
            }
            else
            {
                if (isEmpty()==false && bag.ant == null && bag.next.ant != null && bag.prev.ant==null)
                {
                    bag.ant = this.pop().ant;
                }
            }
        }
        public void kontrol()
        {
            dusme();
            cıkma();
        }
    }
    class tuzak : cukur
    {

        public tuzak() : base(1)
        {
        }
        public node peek()
        {
            node f = this.pop();
            this.push(f);
            return f;
        }
        public bool kapan()
        {
            if (this.dusme()==true)
            {
                karınca ka = this.peek().ant;
                ka.life--;
                if (ka.life == 0)
                {
                    this.peek().ant = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Karınca sayısını giriniz:");
            string e = Console.ReadLine();
            int karınca_say = Int16.Parse(e);
            node[] sıra = {new node(), new node(), new node(new cukur(3)), new node(), new node(new cukur(2)),
                new node(),new node(new tuzak()),new node(), new node(new cukur(4)),new node() };
            
            int yol_uzunlugu = 0;
            ArrayList tuzaklar = new ArrayList();
            ArrayList cukurlu = new ArrayList();
            string er = "";
            while (er!="q")
            {
                Console.WriteLine("Yolun içeriğini girin(tuzak=t/yol=y/cukur=c/cıkıs=q):");
                er = Console.ReadLine();
                if (er=="c")
                {
                    Console.WriteLine("Çukur Büyüklüğü:");
                    string sayı = Console.ReadLine();
                    sıra[yol_uzunlugu] = new node(new cukur(Int32.Parse(sayı)));
                    cukurlu.Add(yol_uzunlugu);
                }
                else if (er=="t")
                {
                    sıra[yol_uzunlugu] = new node(new tuzak());
                    tuzaklar.Add(yol_uzunlugu);
                }
                else if(er == "y")
                {
                    sıra[yol_uzunlugu] = new node();
                }
                yol_uzunlugu++;
            }
            for(int f=1;f<yol_uzunlugu;f++)
            {
                sıra[f-1].next = sıra[f];
                sıra[f].prev = sıra[f - 1];
            }
            karınca[] dizi = new karınca[karınca_say];
            for (int i= 0;i<karınca_say;i++)
            {
                dizi[i] = new karınca(i);
            }
            bool finish()
            {
                bool fin = true;
                foreach (node y in sıra)
                {
                    if (y.ant!=null)
                    {
                        fin = false;
                        break;
                    }
                }
                return fin;
            }
            node starter = new node();
            starter.next = sıra[0];
            node print = new node();
            sıra[yol_uzunlugu-1].next = print;
            print.next = null;
            void tostring()
            {
                if (print.ant!=null)
                {
                    Console.WriteLine(print.ant.seq);
                }
            }
            void ilerle(node[] ndd,int ss)
            {
                if (ss < karınca_say)
                {
                    starter.ant = dizi[ss];
                }
                else
                {
                    starter.ant = null;
                }
                node t = starter;
                karınca temp1 = t.ant;
                while (t.next != null)
                {
                    karınca temp2 = t.next.ant;
                    t.next.ant = temp1;
                    temp1 = temp2;
                    t = t.next;
                }
                
            }
            int s = 0;
            do
            {
                ilerle(sıra, s);
                foreach(int f in cukurlu)
                {
                    sıra[f].baglantı.kontrol();
                }
                foreach (int v in tuzaklar)
                {
                    if (sıra[v].baglantı != null)
                    {
                        sıra[v].yakala();
                        sıra[v].baglantı.kontrol();
                    }
                }
                tostring();
                s++;
            }while (finish() == false);
                Console.ReadKey();
        }
    }
}
