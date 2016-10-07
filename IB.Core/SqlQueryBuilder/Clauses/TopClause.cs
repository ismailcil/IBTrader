using System;
using System.Collections.Generic;
using System.Text;
using IB.Core.SqlQueryBuilder.Enums;

//
// Class: TopClause
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
//
namespace IB.Core.SqlQueryBuilder.Clauses
{
    /// <summary>
    /// Represents a TOP clause for SELECT statements
    /// </summary>
    public struct TopClause
    {
        public int Quantity;
        public TopUnit Unit;
        public TopClause(int nr)
        {
            Quantity = nr;
            Unit = TopUnit.Records;
        }
        public TopClause(int nr, TopUnit aUnit)
        {
            Quantity = nr;
            Unit = aUnit;
        }
    }

}
