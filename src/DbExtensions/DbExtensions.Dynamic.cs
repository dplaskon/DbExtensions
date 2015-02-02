﻿// Copyright 2013-2014 Max Toro Q.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace DbExtensions {

   public static partial class Extensions {

      /// <summary>
      /// Maps the results of the <paramref name="command"/> to dynamic objects.
      /// The query is deferred-executed.
      /// </summary>
      /// <param name="command">The query command.</param>
      /// <returns>The results of the query as dynamic objects.</returns>
      public static IEnumerable<dynamic> Map(this IDbCommand command) {
         return Map(command, (TextWriter)null);
      }

      /// <inheritdoc cref="Map(IDbCommand)"/>
      /// <param name="logger">A <see cref="TextWriter"/> used to log when the command is executed.</param>
      public static IEnumerable<dynamic> Map(this IDbCommand command, TextWriter logger) {

         var mapper = new DynamicMapper { 
            Log = logger
         };

         return Map(command, r => (dynamic)mapper.Map(r), logger);
      }
   }

   partial class Database {

      /// <summary>
      /// Maps the results of the <paramref name="query"/> to dynamic objects.
      /// The query is deferred-executed.
      /// </summary>
      /// <param name="query">The query.</param>
      /// <returns>The results of the query as dynamic objects.</returns>
      /// <seealso cref="Extensions.Map(IDbCommand, TextWriter)"/>
      public IEnumerable<dynamic> Map(SqlBuilder query) {

         var mapper = new DynamicMapper { 
            Log = this.Log
         };

         return Extensions.Map<dynamic>(CreateCommand, query, mapper, this.Log);
      }
   }

   class DynamicMapper : Mapper {

      protected override Node CreateRootNode() {
         return DynamicNode.Root();
      }

      protected override Node CreateSimpleProperty(Node container, string propertyName, int columnOrdinal) {
         return DynamicNode.Simple(columnOrdinal, propertyName);
      }

      protected override Node CreateComplexProperty(Node container, string propertyName) {
         return DynamicNode.Complex(propertyName);
      }

      protected override Node CreateParameterNode(ParameterInfo paramInfo) {
         throw new NotImplementedException();
      }

      protected override Node CreateParameterNode(int columnOrdinal, ParameterInfo paramInfo) {
         throw new NotImplementedException();
      }

      protected override CollectionNode CreateCollectionNode(Node container, string propertyName) {
         throw new NotSupportedException();
      }
   }

   class DynamicNode : Node {

      static readonly string _TypeName = typeof(ExpandoObject).FullName;

      readonly string _PropertyName;

      bool _IsComplex;
      int _ColumnOrdinal;

      public override bool IsComplex {
         get { return _IsComplex; }
      }

      public override string PropertyName {
         get { return _PropertyName; }
      }

      public override int ColumnOrdinal {
         get { return _ColumnOrdinal; }
      }

      public override string TypeName {
         get { return _TypeName; }
      }

      public static DynamicNode Root() {

         var node = new DynamicNode() {
            _IsComplex = true,
         };

         return node;
      }

      public static DynamicNode Complex(string propertyName) {

         var node = new DynamicNode(propertyName) {
            _IsComplex = true,
         };

         return node;
      }

      public static DynamicNode Simple(int columnOrdinal, string propertyName) {

         var node = new DynamicNode(propertyName) {
            _ColumnOrdinal = columnOrdinal
         };

         return node;
      }

      private DynamicNode() { }

      private DynamicNode(string propertyName) 
         : this() {

         if (propertyName == null) throw new ArgumentNullException("propertyName");
         if (propertyName.Length == 0) throw new ArgumentException("Cannot map column using an empty property name.", "propertyName");

         uint nameAsNumber;

         if (UInt32.TryParse(propertyName, out nameAsNumber)) {
            throw new ArgumentException("Cannot use constructor mapping, by using numeric column names, unless you specify the type of the object you want to map to.", "propertyName");
         }

         this._PropertyName = propertyName;
      }

      public override object Create(IDataRecord record, MappingContext context) {
         return new ExpandoObject();
      }

      protected override object Get(ref object instance) {

         var dictionary = (IDictionary<string, object>)instance;

         object value;

         if (dictionary.TryGetValue(PropertyName, out value)) {
            return value;
         }

         return null;
      }

      protected override void Set(ref object instance, object value, MappingContext context) {
         ((IDictionary<string, object>)instance)[PropertyName] = value;
      }

      public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) {
         throw new InvalidOperationException("Cannot use constructor mapping, by using numeric column names, unless you specify the type of the object you want to map to.");
      }
   }
}
