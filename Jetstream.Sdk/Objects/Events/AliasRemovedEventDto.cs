﻿/*
    Copyright 2023 Terso Solutions, Inc.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

namespace TersoSolutions.Jetstream.Sdk.Objects.Events
{
    /// <summary>
    /// The Alias Removed Event is generated when an alias is removed from an application.
    /// </summary>
    public class AliasRemovedEventDto : EventDto
    {
        /// <summary>
        /// Autogenerated ID associated to the Alias
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Name of the alias removed
        /// </summary>
        public string Alias { get; set; }
    }
}
