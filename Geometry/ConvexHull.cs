using Algorithms.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Algorithms.Geometry
{
    /// <summary>
    /// Convex Hull 생성을 위한 정적함수
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Convex_hull"/>
    public static class ConvexHull
    {
        /// <summary>
        /// 좌표를 갖는 대상에 대해 ConvexHull 정보를 생성합니다.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static List<TItem> CreateConvexHull<TItem>(
            List<TItem> items, Func<TItem, Point> selector)
        {
            if (items.Count <= 2)
                return items.ToList();

            var sorted = Sort(items, selector);
            var stack = new Stack<TItem>(sorted.Count);

            stack.Push(sorted[0]);
            stack.Push(sorted[1]);

            int idx = 2;
            while (idx < sorted.Count)
            {
                var item1 = stack.Pop();
                var item2 = stack.Pop();
                var nextItem = sorted[idx];

                // * 정상 단계
                // pt1, pt2와 이루는 각도가 좌측 방향인 경우 
                // pt1, pt2, next를 모두 stack에 넣고 다음 단계 진행
                if (IsCCW(item2, item1, nextItem, selector))
                {
                    stack.Push(item2);
                    stack.Push(item1);
                    stack.Push(nextItem);
                    idx++;
                }
                // * 비정상 단계
                // 좌측 방향이 아니며, Stack이 비어있지 않을 경우
                // pt1로부터 다음 pt2에 대한 단계 진행
                else if (stack.Any())
                {
                    stack.Push(item2);
                }
                // * 특별 케이스
                // sorted[0]와 sorted[1] 사이를 잇는 선 위에 next가 존재하는 경우
                // ex) sorted[0] : (0, 0), sorted[1] : (3, 0), next : (1, 0)
                // (Sort 함수 특성상 탐색 첫 단계에서만 해당 경우가 발생할 수 있기 때문에 Stack이 비어있음)
                else
                {
                    stack.Push(item2);
                    stack.Push(item1);
                    idx++;
                }
            }

            return stack.Reverse().ToList();
        }

        /// <summary>
        /// item1, item2, item3의 위치에 따라 세 대상이 이루는 각도가 CCW인지 확인합니다.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <param name="item3"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static bool IsCCW<TItem>(
            TItem item1, TItem item2, TItem item3, Func<TItem, Point> selector)
            => PointHelper.IsCCW(selector(item1), selector(item2), selector(item3));

        /// <summary>
        /// ConvexHull을 생성하기 위해 좌하단 item을 기준으로 시계반대방향으로 목록을 정렬합니다.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static List<TItem> Sort<TItem>(
            List<TItem> items, Func<TItem, Point> selector)
        {
            // 좌하단 기준점 수집
            var first = items
                .MinItems(item => selector(item).Y)
                .MinItem(item => selector(item).X);
            
            return items
                .Where(i => !i.Equals(first))
                .OrderBy(i => PointHelper.GetDegree(selector(i), selector(first)))
                .ThenBy(i => PointHelper.GetDistance(selector(i), selector(first)))
                .ToList();
        }
    }

}
