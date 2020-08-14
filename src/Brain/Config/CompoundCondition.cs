using System;
using System.Collections.Generic;
using System.Linq;
using Autoccultist.Yaml;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Autoccultist.Brain.Config
{
    [DuckTypeKeys(new[] { "allOf", "anyOf", "oneOf" })]
    public class CompoundCondition : IGameStateConditionConfig, IYamlConvertible
    {
        public ConditionMode Mode { get; set; }
        public List<IGameStateConditionConfig> Requirements { get; set; } = new List<IGameStateConditionConfig>();

        public void Validate()
        {
            if (this.Requirements == null || this.Requirements.Count == 0)
            {
                throw new InvalidConfigException("CompoundCondition must have requirements.");
            }

            foreach (var requirement in this.Requirements)
            {
                requirement.Validate();
            }
        }

        public bool IsConditionMet(IGameState state)
        {
            switch (this.Mode)
            {
                case ConditionMode.AllOf:
                    return this.Requirements.All(condition => condition.IsConditionMet(state));
                case ConditionMode.AnyOf:
                    return this.Requirements.Any(condition => condition.IsConditionMet(state));
                case ConditionMode.NoneOf:
                    return !this.Requirements.Any(condition => condition.IsConditionMet(state));
                default:
                    throw new NotImplementedException($"Condition mode {this.Mode} is not implemented.");
            }
        }

        void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            parser.Consume<MappingStart>();

            var key = parser.Consume<Scalar>();
            switch (key.Value)
            {
                case "allOf":
                    this.Mode = ConditionMode.AllOf;
                    break;
                case "anyOf":
                    this.Mode = ConditionMode.AnyOf;
                    break;
                case "noneOf":
                    this.Mode = ConditionMode.NoneOf;
                    break;
                default:
                    throw new YamlException(key.Start, key.End, "GameStateCondition must have one of the following keys: \"allOf\", \"anyOf\", \"oneOf\".");
            }

            this.Requirements = (List<IGameStateConditionConfig>)nestedObjectDeserializer(typeof(List<IGameStateConditionConfig>));

            if (parser.Accept<Scalar>(out var _))
            {
                throw new YamlException(key.Start, key.End, "GameStateCondition must only have one property.");
            }

            parser.Consume<MappingEnd>();
        }

        void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            throw new NotSupportedException();
        }
    }

    public enum ConditionMode
    {
        AnyOf,
        AllOf,
        NoneOf
    }
}