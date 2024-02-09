using System;

/*
 * Interface of StoryTeller. Has to methods: 
 *  - Run. Will be called on start of every game (race). 
 *  - Stop. Stopping internal loop 
 */
public interface IStoryTeller
{
    /// <summary>
    /// Every time, StoryTeller will decide to create obstacle, he will call callback from params of this method.
    /// </summary>
    /// <param name="callback"></param>
    void Run(Action<ObstaclePattern> callback);
    void Stop();
}
