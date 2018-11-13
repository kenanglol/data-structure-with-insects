using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    class bocek
    {
        public int life;
        public int seq;
        public string isim;
        public string yon;
        public bocek(int sıra)
        {
            this.seq = sıra;

        }
    }
    class karınca : bocek
    {
        public karınca(int sıra) : base(sıra)
        {
            this.life = 3;
            this.isim = "karınca";
            this.yon = "sag";
        }
    }
    class arı : bocek
    {
        public arı(int sıra) : base(sıra)
        {
            this.life = 4;
            this.isim = "arı";
            this.yon = "sol";
        }
    }
    class yaban_arı : bocek
    {
        public yaban_arı(int sıra,string güz) : base(sıra)
        {
            this.life = 5;
            this.isim = "yaban arı";
            this.yon = güz;
        }
    }
    class node
    {
        public karınca ant;
        public arı bee;
        public yaban_arı y_bee;
        public node next;
        public node prev;
        public cukur baglantı;
        public tuzak pusu;
        public node(cukur dip)
        {
            ant = null;
            bee = null;
            y_bee = null;
            next = null;
            this.baglantı = dip;
            this.baglantı.bag = this;
        }
        public node()
        {
            ant = null;
            bee = null;
            y_bee = null;
            next = null;
        }
        public node(tuzak t)
        {
            ant = null;
            bee = null;
            y_bee = null;
            this.ant = null;
            next = null;
            pusu = t;
            t.bag = this;
        }
        public void yakala()
        {
            if ((pusu.kapan() == true))
            {
                pusu = null;
            }
        }
        public bool bos()
        {
            if (bee==null && y_bee==null && ant==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bocek dondur()
        {
            if (bee != null)
            {
                return bee;
            }
            else if (y_bee != null)
            {
                return y_bee;
            }
            else
            {
                return ant;
            }
        }
        public void sil()
        {

            if (bee != null)
            {
                bee = null;
            }
            else if (y_bee != null)
            {
                y_bee = null;
            }
            else
            {
                ant = null;
            }
            
        }
        public void ekle(bocek b)
        {
            if (b.isim == "karınca")
            {
                ant = (karınca)b;
            }
            else if (b.isim == "arı")
            {
                bee = (arı)b;
            }
            else
            {
                y_bee = (yaban_arı)b;
            }
        }
        private void oldur(karınca ka,arı ar,yaban_arı ya){
            if(ka!=null && ka.life<=0)
            {
                ka = null;
            }
            if(ar!=null && ar.life<=0)
            {
                ar = null;
            }
            if(ya!=null && ya.life<=0)
            {
                ya = null;
            }

        }
        public void savas()
        {
            if(ant!=null && bee!=null && y_bee!=null)
            {
                int hasar=(ant.life + bee.life + y_bee.life)/3;
                ant.life = ant.life - hasar;
                bee.life = bee.life - hasar;
                y_bee.life = y_bee.life - hasar;
                if (ant.life <= 0)
                {
                    ant = null;
                }
                if (bee.life <= 0)
                {
                    bee = null;
                }
                if (y_bee.life <= 0)
                {
                    y_bee= null;
                }
            }
            else if(ant != null && bee != null && y_bee == null)
            {
                int hasar = (ant.life + bee.life) / 2;
                ant.life = ant.life - hasar;
                bee.life = bee.life - hasar;
                if (ant.life <= 0)
                {
                    ant = null;
                }
                if (bee.life <= 0)
                {
                    bee = null;
                }
            }
            else if (ant != null && bee == null && y_bee != null)
            {
                int hasar = (ant.life + y_bee.life) / 2;
                ant.life = ant.life - hasar;
                y_bee.life = y_bee.life - hasar;
                if (ant.life <= 0)
                {
                    ant = null;
                }
                if (y_bee.life <= 0)
                {
                    y_bee = null;
                }
            }
            else if (ant == null && bee != null && y_bee != null)
            {
                int hasar = (bee.life + y_bee.life) / 2;
                bee.life = bee.life - hasar;
                y_bee.life = y_bee.life - hasar;
                if (bee.life <= 0)
                {
                    bee = null;
                }
                if (y_bee.life <= 0)
                {
                    y_bee = null;
                }
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
        public bool push(bocek b)
        {
            node element = new node();
            if (b.isim=="karınca")
            {
                element.ant = (karınca)b;
            }
            else if (b.isim == "arı")
            {
                element.bee = (arı)b;
            }
            else
            {
                element.y_bee = (yaban_arı)b;
            }
            if (isEmpty() == true)
            {
                first = element;
                last = element;
                len++;
                return true;
            }
            if (len + 1 < max || len + 1 == max)
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
        public bool push(node element)
        {
            if (isEmpty() == true)
            {
                first = element;
                last = element;
                len++;
                return true;
            }
            if (len + 1 < max || len + 1 == max)
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
            if (len == 0)
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
            if (len == 0)
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
        public void dusme()
        {
            if (isFull() == false && bag.bos()==false)
            {
                push(bag.dondur());
                bag.sil();
            }
        }
        public void cıkma()
        {
            if (isFull() == true)
            {
                if (bag.bos()==true && bag.next.bos()!=false)
                {
                    bag.ekle(this.pop().dondur());
                }
            }
            else
            {
                if (isEmpty() == false && bag.bos()==true  && bag.next.bos()==false )
                {
                    bag.ekle(this.pop().dondur());
                }
            }
        }
        public void kontrol()
        {
            dusme();
            cıkma();
        }
    }
    class tuzak
    {
        public node bag;
        node tuzz;
        public tuzak()
        {
            tuzz = new node();
        }
        public bool kapan()
        {
            bocek d = bag.dondur();
            if (d != null)
            {
                d.life--;
                if (d.life == 0)
                {
                    bag.sil();
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
        static void atma(List<bocek> atılan,List<bocek> silinen)
        {
            if (silinen.Count!=0)
            {
                foreach (bocek y in silinen)
                {
                    atılan.Add(y);
                    silinen.Remove(y);
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Karınca sayısını giriniz:");
            string e = Console.ReadLine();
            int karınca_say = Int16.Parse(e);
            Console.WriteLine("Yol Uzunluğunu giriniz:");
            string yol=Console.ReadLine();
            Console.WriteLine("Yaban arıları kaçıncı odadan girsin:");
            string c = Console.ReadLine();
            int indeks = Int16.Parse(c);
            int yol_uzunlugu = Int16.Parse(yol);
            node[] sıra = new node[yol_uzunlugu];
            ArrayList tuzaklar = new ArrayList();
            ArrayList cukurlu = new ArrayList();
            string er = "";
            int a = 0;
            
            while (a!=yol_uzunlugu)
            {
                Console.WriteLine("Yolun içeriğini girin(tuzak=t/yol=y/cukur=c):");
                er = Console.ReadLine();
                if (er == "c")
                {
                    Console.WriteLine("Çukur Büyüklüğü:");
                    string sayı = Console.ReadLine();
                    sıra[a] = new node(new cukur(Int32.Parse(sayı)));
                    cukurlu.Add(a);
                }
                else if (er == "t")
                {
                    sıra[a] = new node(new tuzak());
                    tuzaklar.Add(a);
                }
                else if (er == "y")
                {
                    sıra[a] = new node();
                }a++;
            }
            for (int f = 1; f < yol_uzunlugu; f++)
            {
                sıra[f - 1].next = sıra[f];
                sıra[f].prev = sıra[f - 1];
            }
            karınca[] dizi = new karınca[karınca_say];
            for (int i = 0; i < karınca_say; i++)
            {
                dizi[i] = new karınca(i);
            }
            arı[] arı_dizi = {new arı(0),new arı(1),new arı(2),new arı(3) };
            yaban_arı[] yaban_dizi = { new yaban_arı(1,"sol"),new yaban_arı(2,"sag"),new yaban_arı(3,"sol")};
            node starter = new node();
            starter.prev = null;
            starter.next = sıra[0];
            sıra[0].prev = starter;
            node print = new node();
            sıra[yol_uzunlugu - 1].next = print;
            print.prev = sıra[yol_uzunlugu - 1];
            print.next = null;
            bool finish()
            {
                bool fin = true;
                node t = starter;
                while(t!=null)
                {
                    if(t.ant!=null || t.y_bee!=null || t.bee!=null)
                    {
                        fin = false;
                        break;
                    }
                    t = t.next;
                }
                return fin;
            }
            void tostring()
            {
                if (print.ant!=null)
                {
                        Console.WriteLine(print.ant.isim+","+print.ant.seq);
                        print.ant = null;
                }
                if (print.y_bee != null)
                {
                        Console.WriteLine(print.y_bee.isim + "," + print.y_bee.seq);
                        print.y_bee = null;
                }
                if (starter.bee!=null)
                {
                    Console.WriteLine(starter.bee.isim+","+starter.bee.seq);
                    starter.ant = null;
                }
                if (starter.y_bee != null)
                {
                    Console.WriteLine(starter.y_bee.isim + "," + starter.y_bee.seq);
                    starter.y_bee = null;
                }
            }
            void ilerle_sol(node[] ndd, int ss)
            {
                if (ss < 4)
                {
                    print.bee=arı_dizi[ss];
                }
                if(ss<3){
                    sıra[indeks].y_bee = yaban_dizi[ss];
                }
                node t = print;
                arı temp = print.bee;
                print.bee = null;
                arı temp1=null;
                yaban_arı temp2 =sıra[indeks].y_bee;
                yaban_arı temp3 = null;
                while (t.prev != null)
                {
                    temp1 = t.prev.bee;
                    t.prev.bee = temp;
                    temp = temp1;
                    if(t.y_bee!=null &&t.y_bee.yon=="sol"){
                        temp3 = t.prev.y_bee;
                        t.prev.y_bee = temp2;
                        temp2 = temp3;
                        temp3 = null;
                    }

                    t = t.prev;
                }
            }
            void ilerle_sag(node[] ndd, int ss)
            {
                if (ss < karınca_say)
                {
                    starter.ant = dizi[ss];
                }
                node t = starter;
                karınca temp2 = starter.ant;
                starter.ant = null;
                karınca temp3 = null;
                yaban_arı temp = sıra[indeks].y_bee;
                yaban_arı temp1 = null;
                while (t.next != null)
                {
                    temp3 = t.next.ant;
                    t.next.ant = temp2;
                    temp2 = temp3;
                    temp3 = null;
                    if (t.y_bee != null && t.y_bee.yon == "sag")
                    {
                        temp1 = t.next.y_bee;
                        t.next.y_bee = temp;
                        temp = temp1;
                    }
                    t = t.next;
                }
            }
            int s = 0;
            do
            {
                foreach(node n in sıra)
                {
                    n.savas();
                }
                tostring();
                ilerle_sag(sıra, s);
                ilerle_sol(sıra, s);
                foreach (int f in cukurlu)
                {
                    sıra[f].baglantı.kontrol();
                }
                foreach (int v in tuzaklar)
                {
                    if (sıra[v].pusu != null)
                    {
                        sıra[v].yakala();
                    }
                }
                s++;
            } while (finish() == false);
            Console.ReadKey();
        }
    }
}
