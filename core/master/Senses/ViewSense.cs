using System.Threading.Tasks;
using Proto;

namespace Nasdan.Core.Senses
{
    public class ImageMessage
    {

        public string Url { get; set; }

    }


    public class ViewSense {

        void Process(ImageMessage img)
        {
            //Guardamos lo recibido en Solr. 
                //-Usuario que lo envia (sesión), lo suyo es que se guarde por sesión las conversaciones y las imagenes recibidas en esa sesión
                //-Buscamos en nuestros conocimientos esa imagen tal cual a ver si ya ha sido analizada antes. Si
                //no existe creamos un nuevo grafo y lo enlazamos a lo metido en Solr. Ese grafo tiene todas las
                //propiedades percibidas pero sin categorizar.
        }
        

    }

}