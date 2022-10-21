#nullable disable

using Blueprint41.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Neo4j.Refactoring.Templates
{
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    public abstract class TemplateBase
    {
        protected TemplateBase()
        {
            OutputParameters = new Dictionary<string, object>();
        }
        public Dictionary<string, object> OutputParameters { get; private set; }

        public abstract string TransformText();

        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField is null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField is null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField is null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0)
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent is null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField;
                }
                set
                {
                    if ((value is not null))
                    {
                        this.formatProviderField = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert is null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method is null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion

        protected void Log(string message, params object[] args) => Parser.Log(message, args);
    }

    public abstract class TemplateBase<TSelf> : TemplateBase
        where TSelf : TemplateBase<TSelf>
    {
        internal Func<TSelf> CreateInstance { get; set; }
        internal Action<TSelf> Setup { get; set; }
        public void Run(bool withTransaction = true)
        {
            if (!Parser.ShouldExecute)
                return;

            if (withTransaction)
            {
                using (Transaction.Begin(withTransaction))
                {
                    Execute();
                    Transaction.Commit();
                }
            }
            else
            {
                Execute();
            }
        }
        public void RunBatched()
        {
            if (!Parser.ShouldExecute)
                return;

            RawResultStatistics counters;
            do
            {
                using (Transaction.Begin(true))
                {
                    TSelf template = CreateInstance();
                    Setup.Invoke(template);

                    RawResult result = template.Execute();
                    counters = result.Statistics();
                    Transaction.Commit();
                }
            }
            while (counters.ContainsUpdates);
        }

        private RawResult Execute()
        {
            if (!Parser.ShouldExecute)
                return null;

            string cypher = TransformText();

            if (OutputParameters.Count == 0)
                return Transaction.RunningTransaction.Run(cypher);
            else
                return Transaction.RunningTransaction.Run(cypher, OutputParameters);
        }
    }

    public abstract class ApplyFunctionalIdBase : TemplateBase<ApplyFunctionalIdBase>
    {
        public Entity Entity { get; set; }
        public FunctionalId FunctionalId { get; set; }
        public bool Full { get; set; }
    }
    public abstract class ConvertBase : TemplateBase<ConvertBase>
    {
        public Entity Entity { get; set; }
        public Property Property { get; set; }
        public string WhereScript { get; set; }
        public string AssignScript { get; set; }
    }
    public abstract class CopyPropertyBase : TemplateBase<CopyPropertyBase>
    {
        public Entity Entity { get; set; }
        public string To { get; set; }
        public string From { get; set; }
    }
    public abstract class CreateIndexBase : TemplateBase<CreateIndexBase>
    {
        public Entity Entity { get; set; }
        public Property Property { get; set; }
    }
    public abstract class CreateUniqueConstraintBase : TemplateBase<CreateUniqueConstraintBase>
    {
        public Entity Entity { get; set; }
        public Property Property { get; set; }
    }
    public abstract class DropExistConstraintBase : TemplateBase<DropExistConstraintBase>
    {
        public Property Property { get; set; }
    }
    public abstract class MergePropertyBase : TemplateBase<MergePropertyBase>
    {
        public Entity ConcreteParent { get; set; }
        public Property From { get; set; }
        public Property To { get; set; }
        public MergeAlgorithm MergeAlgorithm { get; set; }
    }
    public abstract class MergeRelationshipBase : TemplateBase<MergeRelationshipBase>
    {
        public Relationship From { get; set; }
        public Relationship To { get; set; }
        public MergeAlgorithm MergeAlgorithm { get; set; }
    }
    public abstract class RemoveEntityBase : TemplateBase<RemoveEntityBase>
    {
        public string Name { get; set; }
    }
    public abstract class RemovePropertyBase : TemplateBase<RemovePropertyBase>
    {
        public Entity ConcreteParent { get; set; }
        public string Name { get; set; }
    }
    public abstract class RemoveRelationshipBase : TemplateBase<RemoveRelationshipBase>
    {
        public string InEntity { get; set; }
        public string Relation { get; set; }
        public string OutEntity { get; set; }
    }
    public abstract class RenameEntityBase : TemplateBase<RenameEntityBase>
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
    public abstract class RenamePropertyBase : TemplateBase<RenamePropertyBase>
    {
        public Entity ConcreteParent { get; set; }
        public Property From { get; set; }
        public string To { get; set; }
    }
    public abstract class RenameRelationshipBase : TemplateBase<RenameRelationshipBase>
    {
        public Relationship Relationship { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
    public abstract class SetCreationDateBase : TemplateBase<SetCreationDateBase>
    {
    }
    public abstract class SetDefaultConstantValueBase : TemplateBase<SetDefaultConstantValueBase>
    {
        public Entity Entity { get; set; }
        public Property Property { get; set; }
        public object Value { get; set; }
    }
    public abstract class SetDefaultLookupValueBase : TemplateBase<SetDefaultLookupValueBase>
    {
        public Property Property { get; set; }
        public string Value { get; set; }
    }
    public abstract class SetLabelBase : TemplateBase<SetLabelBase>
    {
        public Entity Entity { get; set; }
        public string Label { get; set; }
    }
    public abstract class SetRelationshipPropertyValueBase : TemplateBase<SetRelationshipPropertyValueBase>
    {
        public string RelationshipType { get; set; }
        public string Property { get; set; }
        public object Value { get; set; }
    }
}
