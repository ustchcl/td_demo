    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class BartizanAndBuild {
	public int id; // 编号
	public int purchasBartizan; // 可购买箭塔上限等级
	public int minJudgeLevel; // 最低判定等级
}

[XmlRoot("BartizanAndBuild")]
public class BartizanAndBuildFactory {
    [XmlElement("data")]
    public BartizanAndBuild[] BartizanAndBuildArray {get; set;}
    private static BartizanAndBuildFactory _instance;

    BartizanAndBuildFactory() {}

    const string configXML = @"
    <BartizanAndBuild>
    <data><id>0</id><purchasBartizan>1</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>1</id><purchasBartizan>1</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>2</id><purchasBartizan>2</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>3</id><purchasBartizan>3</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>4</id><purchasBartizan>4</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>5</id><purchasBartizan>5</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>6</id><purchasBartizan>6</purchasBartizan><minJudgeLevel>1</minJudgeLevel></data><data><id>7</id><purchasBartizan>7</purchasBartizan><minJudgeLevel>2</minJudgeLevel></data><data><id>8</id><purchasBartizan>8</purchasBartizan><minJudgeLevel>3</minJudgeLevel></data><data><id>9</id><purchasBartizan>9</purchasBartizan><minJudgeLevel>4</minJudgeLevel></data><data><id>10</id><purchasBartizan>10</purchasBartizan><minJudgeLevel>5</minJudgeLevel></data><data><id>11</id><purchasBartizan>11</purchasBartizan><minJudgeLevel>6</minJudgeLevel></data><data><id>12</id><purchasBartizan>12</purchasBartizan><minJudgeLevel>7</minJudgeLevel></data><data><id>13</id><purchasBartizan>13</purchasBartizan><minJudgeLevel>8</minJudgeLevel></data><data><id>14</id><purchasBartizan>14</purchasBartizan><minJudgeLevel>9</minJudgeLevel></data><data><id>15</id><purchasBartizan>15</purchasBartizan><minJudgeLevel>10</minJudgeLevel></data><data><id>16</id><purchasBartizan>16</purchasBartizan><minJudgeLevel>11</minJudgeLevel></data><data><id>17</id><purchasBartizan>17</purchasBartizan><minJudgeLevel>12</minJudgeLevel></data><data><id>18</id><purchasBartizan>18</purchasBartizan><minJudgeLevel>13</minJudgeLevel></data><data><id>19</id><purchasBartizan>19</purchasBartizan><minJudgeLevel>14</minJudgeLevel></data><data><id>20</id><purchasBartizan>20</purchasBartizan><minJudgeLevel>15</minJudgeLevel></data><data><id>21</id><purchasBartizan>21</purchasBartizan><minJudgeLevel>16</minJudgeLevel></data><data><id>22</id><purchasBartizan>22</purchasBartizan><minJudgeLevel>17</minJudgeLevel></data><data><id>23</id><purchasBartizan>23</purchasBartizan><minJudgeLevel>18</minJudgeLevel></data><data><id>24</id><purchasBartizan>24</purchasBartizan><minJudgeLevel>19</minJudgeLevel></data><data><id>25</id><purchasBartizan>25</purchasBartizan><minJudgeLevel>20</minJudgeLevel></data><data><id>26</id><purchasBartizan>26</purchasBartizan><minJudgeLevel>21</minJudgeLevel></data><data><id>27</id><purchasBartizan>27</purchasBartizan><minJudgeLevel>22</minJudgeLevel></data><data><id>28</id><purchasBartizan>28</purchasBartizan><minJudgeLevel>23</minJudgeLevel></data><data><id>29</id><purchasBartizan>29</purchasBartizan><minJudgeLevel>24</minJudgeLevel></data><data><id>30</id><purchasBartizan>30</purchasBartizan><minJudgeLevel>25</minJudgeLevel></data><data><id>31</id><purchasBartizan>31</purchasBartizan><minJudgeLevel>26</minJudgeLevel></data><data><id>32</id><purchasBartizan>32</purchasBartizan><minJudgeLevel>27</minJudgeLevel></data><data><id>33</id><purchasBartizan>33</purchasBartizan><minJudgeLevel>28</minJudgeLevel></data><data><id>34</id><purchasBartizan>34</purchasBartizan><minJudgeLevel>29</minJudgeLevel></data><data><id>35</id><purchasBartizan>35</purchasBartizan><minJudgeLevel>30</minJudgeLevel></data><data><id>36</id><purchasBartizan>36</purchasBartizan><minJudgeLevel>31</minJudgeLevel></data><data><id>37</id><purchasBartizan>37</purchasBartizan><minJudgeLevel>32</minJudgeLevel></data><data><id>38</id><purchasBartizan>38</purchasBartizan><minJudgeLevel>33</minJudgeLevel></data><data><id>39</id><purchasBartizan>39</purchasBartizan><minJudgeLevel>34</minJudgeLevel></data><data><id>40</id><purchasBartizan>40</purchasBartizan><minJudgeLevel>35</minJudgeLevel></data><data><id>41</id><purchasBartizan>41</purchasBartizan><minJudgeLevel>36</minJudgeLevel></data><data><id>42</id><purchasBartizan>42</purchasBartizan><minJudgeLevel>37</minJudgeLevel></data><data><id>43</id><purchasBartizan>43</purchasBartizan><minJudgeLevel>38</minJudgeLevel></data><data><id>44</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data><data><id>45</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data><data><id>46</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data><data><id>47</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data><data><id>48</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data><data><id>49</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data><data><id>50</id><purchasBartizan>44</purchasBartizan><minJudgeLevel>39</minJudgeLevel></data>
</BartizanAndBuild>
    ";

    public static BartizanAndBuildFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(BartizanAndBuildFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as BartizanAndBuildFactory;
            }
            return _instance;
        }
    }
}
    