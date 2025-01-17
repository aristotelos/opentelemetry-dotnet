// <copyright file="DelegatingExporter.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
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
// </copyright>

using System;

namespace OpenTelemetry.Tests;

internal sealed class DelegatingExporter<T> : BaseExporter<T>
    where T : class
{
    public Func<Batch<T>, ExportResult> OnExportFunc { get; set; } = (batch) => default;

    public Func<int, bool> OnForceFlushFunc { get; set; } = (timeout) => true;

    public Func<int, bool> OnShutdownFunc { get; set; } = (timeout) => true;

    public override ExportResult Export(in Batch<T> batch) => this.OnExportFunc(batch);

    protected override bool OnForceFlush(int timeoutMilliseconds) => this.OnForceFlushFunc(timeoutMilliseconds);

    protected override bool OnShutdown(int timeoutMilliseconds) => this.OnShutdownFunc(timeoutMilliseconds);
}
