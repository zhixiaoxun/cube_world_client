using System.Collections;
using System.Collections.Generic;

namespace Core.RepresentLogic
{
	public class RepresentMap : Singleton<RepresentMap>
    {
        public Dictionary<string, ushort> _ContentIDMap = new Dictionary<string, ushort>();

        protected override IEnumerator OnInitCoroutine()
        {
            InitContentIDMap();

            yield return 1;
        }

        void InitContentIDMap()
        {
            
            //_ContentIDMap["air"] = 0;   //air
            //_ContentIDMap["default:clay"] = 1;   //default:clay
            //_ContentIDMap["default:dirt"] = 1;   //default:dirt
            //_ContentIDMap["default:wood"] = 6;   //default:wood
            //_ContentIDMap["default:jungletree"] = 9;   //default:jungletree
            //_ContentIDMap["default:fence_wood"] = 6;   //default:fence_wood
            //_ContentIDMap["default:tree"] = 9;   //default:tree
            //_ContentIDMap["default:mossycobble"] = 5;   //default:mossycobble
            //_ContentIDMap["default:dirt_with_grass"] = 2;   //default:dirt_with_grass
            //_ContentIDMap["default:stonebrick"] = 3;   //default:stonebrick
            //_ContentIDMap["default:ladder"] = 3;   //default:ladder
            //_ContentIDMap["default:tang_stone"] = 3;   //default:tang_stone
            //_ContentIDMap["stairs:stair_wood"] = 6;   //stairs: stair_wood
            //_ContentIDMap["default:tang_pillar"] = 5;   //default:tang_pillar
            //_ContentIDMap["default:torch"] = 4;   //default:torch
            //_ContentIDMap["default:tang_wa"] = 5;   //default:tang_wa
            //_ContentIDMap["stairs:stair_wood_upside_down"] = 3;   //stairs: stair_wood_upside_down
            //_ContentIDMap["ignore"] = 0;   //ignore
            //_ContentIDMap["default:pinewood"] = 6;   //default:pinewood
            //_ContentIDMap["default:yellow_flower"] = 8;   //default:yellow_flower
            //_ContentIDMap["default:glass"] = 201;   //default:glass
            //_ContentIDMap["default:vines"] = 9;   //default:vines
            //_ContentIDMap["doors:door_oak_wood_t_1"] = 7;   //doors: door_oak_wood_t_1
            //_ContentIDMap["stairs:stair_stonebrick"] = 3;   //stairs: stair_stonebrick
            //_ContentIDMap["stairs:stair_stonebrick_upside_down"] = 3;   //stairs: stair_stonebrick_upside_down
            //_ContentIDMap["stairs:slab_cobblestone_upside_down"] = 3;   //stairs: slab_cobblestone_upside_down
            //_ContentIDMap["stairs:slab_tang_wa"] = 3;   //stairs: slab_tang_wa
            //_ContentIDMap["default:leaves_swamptree"] = 9;   //default:leaves_swamptree
            //_ContentIDMap["default:grass_3"] = 2;   //default:grass_3
            //_ContentIDMap["stairs:stair_tang_wa"] = 5;   //stairs: stair_tang_wa
            //_ContentIDMap["stairs:stair_spruce_wood"] = 5;   //stairs: stair_spruce_wood
            //_ContentIDMap["default:water_source"] = 201;   //default:water_source
            //_ContentIDMap["default:andesite"] = 3;   //default:andesite

            _ContentIDMap["air"] = 0;   //air
            _ContentIDMap["default:clay"] = 11;   //default:clay    粘土 无
            _ContentIDMap["default:dirt"] = 1;   //default:dirt
            _ContentIDMap["default:wood"] = 6;   //default:wood
            _ContentIDMap["default:jungletree"] = 12;   //default:jungletree  丛林树 无 可尝试组合
            _ContentIDMap["default:fence_wood"] = 13;   //default:fence_wood 栅栏木 无
            _ContentIDMap["default:tree"] = 14;   //default:tree             树 无 可尝试组合
            _ContentIDMap["default:mossycobble"] = 15;   //default:mossycobble 生苔鹅卵石 无
            _ContentIDMap["default:dirt_with_grass"] = 16;   //default:dirt_with_grass 长草的泥土 无 可换图
            _ContentIDMap["default:stonebrick"] = 17;   //default:stonebrick 石砖 无
            _ContentIDMap["default:ladder"] = 202;   //default:ladder 梯子 无
            _ContentIDMap["default:tang_stone"] = 19;   //default:tang_stone 唐石 无
            _ContentIDMap["stairs:stair_wood"] = 20;   //stairs: stair_wood 楼梯木 无
            _ContentIDMap["default:tang_pillar"] = 21;   //default:tang_pillar 唐支柱 无
            _ContentIDMap["default:torch"] = 203;   //default:torch 火把 无 特殊
            _ContentIDMap["default:tang_wa"] = 22;   //default:tang_wa 唐瓦 无
            _ContentIDMap["stairs:stair_wood_upside_down"] = 23;   //stairs: stair_wood_upside_down 楼梯木上下 无
            _ContentIDMap["ignore"] = 0;   //ignore
            _ContentIDMap["default:pinewood"] = 24;   //default:pinewood 松木 无
            _ContentIDMap["default:yellow_flower"] = 1;   //default:yellow_flower 黄花 无
            _ContentIDMap["default:glass"] = 26;   //default:glass 玻璃 无
            _ContentIDMap["default:vines"] = 204;   //default:vines 葡萄藤 无
            _ContentIDMap["doors:door_oak_wood_t_1"] = 28;   //doors: door_oak_wood_t_1 门 橡树木 无
            _ContentIDMap["stairs:stair_stonebrick"] = 29;   //stairs: stair_stonebrick 楼梯 石砖 无
            _ContentIDMap["stairs:stair_stonebrick_upside_down"] = 30;   //stairs: stair_stonebrick_upside_down 楼梯 石砖 上下 无
            _ContentIDMap["stairs:slab_cobblestone_upside_down"] = 31;   //stairs: slab_cobblestone_upside_down 楼梯 鹅卵石 上下 无
            _ContentIDMap["stairs:slab_tang_wa"] = 32;   //stairs: slab_tang_wa 楼梯 平板 唐瓦 无
            _ContentIDMap["default:leaves_swamptree"] = 33;   //default:leaves_swamptree 沼泽树叶子 无
            _ContentIDMap["default:grass_3"] = 34;   //default:grass_3 第三种类型草 无
            _ContentIDMap["stairs:stair_tang_wa"] = 35;   //stairs: stair_tang_wa 楼梯 唐瓦 无
            _ContentIDMap["stairs:stair_spruce_wood"] = 36;   //stairs: stair_spruce_wood 楼梯 云杉木 无
            _ContentIDMap["default:water_source"] = 206;   //default:water_source 水 无
            _ContentIDMap["default:andesite"] = 37;   //default:andesite 安山石 无
        }
    }
}
