// This file is part of DePHY Network.

// Copyright (C) Jun Jiang.
// SPDX-License-Identifier: AGPL-3.0-only

// Dephy Blockchain is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Dephy Blockchain is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with Dephy Blockchain.  If not, see <http://www.gnu.org/licenses/>.

//! Dephy Blockchain Node CLI

#![warn(missing_docs)]

use dephy_blockchain as node_cli;

fn main() -> sc_cli::Result<()> {
	node_cli::run()
}
