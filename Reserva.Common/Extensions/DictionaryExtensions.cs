using System;
using System.Collections.Generic;

namespace Reserva.Infra.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Retorna uma instância vazia do tipo especificado caso a chave não esteja presente no dicionário.
        /// Utiliza o método <see cref="Activator.CreateInstance{TValue}"/> em vez de <code>default(TValue)</code>
        /// </summary>
        /// <typeparam name="TKey">Tipo da chave</typeparam>
        /// <typeparam name="TValue">Tipo do valor</typeparam>
        /// <param name="dictionary">Dicionário de referência</param>
        /// <param name="key">Chave para buscar o valor</param>
        /// <returns>O valor presente no dicinário ou uma instância vazia. </returns>
        /// <exception cref="MissingMethodException">The type that is specified for TValue does not have a parameterless constructor.</exception>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : Activator.CreateInstance<TValue>();
        }

        /// <summary>
        /// Retorna o valor especificado caso a chave não esteja presente no dicionário.
        /// </summary>
        /// <typeparam name="TKey">Tipo da chave</typeparam>
        /// <typeparam name="TValue">Tipo do valor</typeparam>
        /// <param name="dictionary">Dicionário de referência</param>
        /// <param name="key">Chave para buscar o valor</param>
        /// <param name="defaultValue">Valor padrão</param>
        /// <returns>O valor presente no dicinário ou uma instância vazia. </returns>
        /// <exception cref="MissingMethodException">The type that is specified for TValue does not have a parameterless constructor.</exception>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>
        (this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// Retorna o valor especificado pelo provedor (<see cref="defaultValueProvider"/>) caso a chave não esteja presente no dicionário.
        /// </summary>
        /// <typeparam name="TKey">Tipo da chave</typeparam>
        /// <typeparam name="TValue">Tipo do valor</typeparam>
        /// <param name="dictionary">Dicionário de referência</param>
        /// <param name="key">Chave para buscar o valor</param>
        /// <param name="defaultValueProvider">Função que gera o valor padrão.</param>
        /// <returns>O valor presente no dicinário ou uma instância vazia. </returns>
        /// <exception cref="MissingMethodException">The type that is specified for TValue does not have a parameterless constructor.</exception>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        /// <exception cref="ArgumentNullException"><see cref="defaultValueProvider"/> is null.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>
        (this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueProvider)
        {
            if (defaultValueProvider == null)
                throw new ArgumentNullException(nameof(defaultValueProvider));

            return dictionary.TryGetValue(key, out var value)
                ? value
                : defaultValueProvider();
        }
    }
}