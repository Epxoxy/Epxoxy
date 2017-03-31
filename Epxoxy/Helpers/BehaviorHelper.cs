using System.Windows;
using System.Windows.Interactivity;

namespace Epxoxy.Helpers
{
    public static class BehaviorHelper
    {
        public static void ApplyBehavior<T>(this DependencyObject p_dependencyObject) where T : Behavior, new()
        {
            if (p_dependencyObject == null)
            {
                return;
            }

            BehaviorCollection itemBehaviors = Interaction.GetBehaviors(p_dependencyObject);

            for (int i = 0; i < itemBehaviors.Count; i++)
            {
                var behavior = itemBehaviors[i];

                if (!(behavior is T))
                {
                    continue;
                }
                itemBehaviors.Remove(behavior);
            }

            itemBehaviors.Add(new T());
        }
    }
}
