using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FN.Client
{
    public abstract class Singleton<Tipo, Interface>
        where Tipo : Interface, new()
    {
        private static readonly object Locker = new object();
        private static Interface instancia;

        /// <summary>
        /// Inicializa uma nova instância do tipo <see cref="Singleton&lt;Tipo, Interface&gt;"/>.
        /// </summary>
        protected Singleton()
        {
            if (!Equals(instancia, default(Interface)))
                throw new InvalidOperationException("Já existe uma instância em execução");
        }

        /// <summary>
        /// Obtém a instância de <see cref="Singleton{Tipo,Interface}"/>. Para
        /// recriar o objeto no seu estado inicial, defina o valor como <c>null</c>.
        /// </summary>
        /// <value>a instancia.</value>
        public static Interface Instancia
        {
            get
            {
                lock (Locker)
                {
                    if (Equals(instancia, default(Interface)))
                        instancia = new Tipo();

                    return instancia;
                }
            }
            set
            {
                instancia = value;
            }
        }

        /// <summary>
        /// Obtém a instância atual da classe <see cref="Singleton{Tipo,Interface}"/>.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Utilize a propriedade Instancia.", false)]
        public static Interface ObtenhaInstancia()
        {
            return Instancia;
        }
    }

    public abstract class Singleton<T> : Singleton<T, T>
        where T : class, new()
    {
    }
}