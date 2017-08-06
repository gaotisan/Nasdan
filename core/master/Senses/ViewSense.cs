using System.Threading.Tasks;

namespace Nasdan.Core.Senses
{
    internal class ViewSense
    {

        void Process(ImageMessage img)
        {
            //Guardamos lo recibido en Solr. 
            
            //-Usuario que lo envia (sesión), lo suyo es que se guarde por sesión las conversaciones y las imagenes recibidas en esa sesión
            //-Buscamos en nuestros conocimientos esa imagen tal cual a ver si ya ha sido analizada antes. Si
            //no existe creamos un nuevo grafo y lo enlazamos a lo metido en Solr. Ese grafo tiene todas las
            //propiedades percibidas pero sin categorizar.
            switch (img.Url.ToLower())
            {
                case "~/images/inputs/colors-black.gif":
                case "~/images/inputs/colors-blue.gif":
                case "~/images/inputs/colors-red.gif":
                case "~/images/inputs/colors-orange.gif":
                case "~/images/inputs/colors-white.gif":
                case "~/images/inputs/colors-green.gif":

                    break;
            }
        }


    }

}