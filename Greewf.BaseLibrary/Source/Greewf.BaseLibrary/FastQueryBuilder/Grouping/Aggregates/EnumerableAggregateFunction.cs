// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Greewf.BaseLibrary.FastQueryBuilder
{
    using System.Linq;
    using System.Linq.Expressions;
    using Infrastructure.Implementation;
    using Infrastructure.Implementation.Expressions;

    /// <summary>
    /// Represents an <see cref="AggregateFunction"/> that uses aggregate extension 
    /// methods provided in <see cref="Enumerable"/>.
    /// </summary>
    public abstract class EnumerableAggregateFunction : EnumerableAggregateFunctionBase
    {
        /// <summary>
        /// Creates the aggregate expression using <see cref="EnumerableAggregateFunctionExpressionBuilder"/>.
        /// </summary>
        /// <param name="enumerableExpression">The grouping expression.</param>
        /// <param name="liftMemberAccessToNull"></param>
        /// <returns></returns>
        public override Expression CreateAggregateExpression(Expression enumerableExpression, bool liftMemberAccessToNull)
        {
            var builder = new EnumerableAggregateFunctionExpressionBuilder(enumerableExpression, this);
            builder.Options.LiftMemberAccessToNull = liftMemberAccessToNull;
            return builder.CreateAggregateExpression();
        }
    }
}